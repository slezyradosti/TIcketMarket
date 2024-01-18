import { makeAutoObservable } from "mobx";
import { Ticket } from "../models/tables/ticket";
import agent from "../api/agent";
import ModuleStore from "./moduleStore";
import { Order } from "../models/tables/order";

class ProfileStore {
    ticketRegistry = new Map<string, Ticket>();
    selectedTicket: Ticket | undefined = undefined;
    orderRegistry = new Map<string, Order>();
    selectedOrder: Order | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingTicketsInitial: boolean = true;
    loadingOrdersInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    get getTicketArray() {
        return Array.from(this.ticketRegistry.values());
    }

    get getOrderArray() {
        return Array.from(this.orderRegistry.values());
    }

    clearTicketData = () => {
        this.ticketRegistry.clear();
        this.loadingTicketsInitial = true;
    }

    clearOrderData = () => {
        this.orderRegistry.clear();
        this.loadingOrdersInitial = true;
    }

    setLoadingTicketsInitial = (state: boolean) => {
        this.loadingTicketsInitial = state;
    }

    setLoadingOrdersInitial = (state: boolean) => {
        this.loadingOrdersInitial = state;
    }

    setLoading = (state: boolean) => {
        this.loading = state;
    }

    loadCustomersTicketList = async () => {
        this.clearTicketData();

        try {
            const result = await agent.Profile.customersList();
            result.forEach(ticket => {
                ModuleStore.convertEntityDateFromApi(ticket);

                this.ticketRegistry.set(ticket.id!, ticket);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingTicketsInitial(false);

        }
    }

    loadCustomersOrderList = async () => {
        this.clearOrderData();

        try {
            const result = await agent.Profile.customersOrderlist();
            result.forEach(order => {
                ModuleStore.convertEntityDateFromApi(order);

                this.orderRegistry.set(order.id!, order);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingOrdersInitial(false);
        }
    }
}

export default ProfileStore;