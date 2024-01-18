import { makeAutoObservable, runInAction } from "mobx";
import { User } from "../models/tables/user";
import { LoginDto } from "../models/DTOs/loginDto";
import agent from "../api/agent";
import { store } from "./store";
import { router } from "../router/routes";
import { RegisterDto } from "../models/DTOs/registerDto";
import { Roles } from "../models/roles";
import ModuleStore from "./moduleStore";

class UserStore {
    user: User | null = null;
    userRights: Roles | undefined = undefined;

    constructor() {
        makeAutoObservable(this)
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: LoginDto) => {
        const user = await agent.Account.login(creds);

        store.commonStore.setToken(user.token!);
        runInAction(() => {
            user.dob = ModuleStore.convertDateFromApi(user.dob);
            this.user = user
        });

        await this.getUserRights();

        //router.navigate('/home');
        store.modalStore.closeModal();
    }

    register = async (creds: RegisterDto) => {
        const user = await agent.Account.register(creds);
        store.commonStore.setToken(user.token!);

        runInAction(() => {
            this.user = user;
            this.userRights = creds.isCustomer ? Roles.Customer : Roles.Seller;
        });

        router.navigate('/home');
        store.modalStore.closeModal();
    }

    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
        this.userRights = undefined;
        router.navigate('/home');
    }

    getUser = async () => {
        try {
            const user = await agent.Account.current();
            runInAction(() => {
                user.dob = ModuleStore.convertDateFromApi(user.dob);
                this.user = user
            });
        } catch (error) {
            console.log(error);
        }
    }

    getUserRights = async () => {
        try {
            const userRights = await agent.Account.getUserRights();
            runInAction(() => this.userRights = userRights.toLocaleLowerCase() as Roles);
        } catch (error) {
            console.log(error);
        }
    }
}

export default UserStore;