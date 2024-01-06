import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../api/agent";
import { Order } from "../../models/tables/order";

class OrderStore {
    orderRegistry = new Map<string, Order>();
    selectedElement: Order | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    clearData = () => {
        this.orderRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadCustomersList = async () => {
        try {
            const result = await agent.Orders.customersOrderlist();
            result.forEach(order => {
                this.orderRegistry.set(order.id!, order);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.orderRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.orderRegistry.get(id)!;
    }

    details = async (id: string) => {
        return await agent.Orders.getCustomersOne(id);
    }

    createOne = async (order: Order) => {
        this.loading = true;

        try {
            await agent.Orders.createOne(order);

            runInAction(() => {
                this.orderRegistry.set(order.id!, order);
                this.selectedElement = order;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }

    deleteOne = async (id: string) => {
        this.loading = true;

        try {
            await agent.Orders.deleteOne(id);
            runInAction(() => {
                this.orderRegistry.delete(id);
                if (this.selectedElement?.id === id) this.cancelSelectedElement();
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }
}

export default OrderStore