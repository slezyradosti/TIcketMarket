import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../api/agent";
import { TicketOrder } from "../../models/tables/ticketOrder";

class TicketOrderStore {
    ticketOrderRegistry = new Map<string, TicketOrder>();
    selectedElement: TicketOrder | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    get getArray() {
        return Array.from(this.ticketOrderRegistry.values());
    }

    clearData = () => {
        this.ticketOrderRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadCustomersList = async () => {
        this.clearData();

        try {
            const result = await agent.TicketOrders.getCustomersList();
            result.forEach(ticketOrder => {
                this.ticketOrderRegistry.set(ticketOrder.id!, ticketOrder);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.ticketOrderRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.ticketOrderRegistry.get(id)!;
    }

    details = async (id: string) => {
        return await agent.TicketOrders.getCustomersOne(id);
    }

    createOne = async (ticketOrder: TicketOrder) => {
        this.loading = true;

        try {
            await agent.TicketOrders.createOne(ticketOrder);

            runInAction(() => {
                this.ticketOrderRegistry.set(ticketOrder.id!, ticketOrder);
                this.selectedElement = ticketOrder;
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

    editOne = async (ticketOrder: TicketOrder) => {
        this.loading = true;

        try {
            await agent.TicketOrders.editOne(ticketOrder);
            runInAction(() => {
                this.ticketOrderRegistry.set(ticketOrder.id!, ticketOrder);
                this.selectedElement = ticketOrder;
                this.editMode = false;
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    deleteOne = async (id: string) => {
        this.loading = true;

        try {
            await agent.TicketOrders.deleteOne(id);
            runInAction(() => {
                this.ticketOrderRegistry.delete(id);
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

export default TicketOrderStore