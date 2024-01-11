import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../api/agent";
import { TicketDiscount } from "../../models/tables/ticketDiscount";

class TicketDiscountStore {
    ticketDiscountRegistry = new Map<string, TicketDiscount>();
    selectedElement: TabTicketDiscountleEvent | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    clearData = () => {
        this.ticketDiscountRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadSellersList = async () => {
        try {
            const result = await agent.TicketDiscounts.getSellersList();
            result.forEach(ticketDiscount => {
                this.ticketDiscountRegistry.set(ticketDiscount.id!, ticketDiscount);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.ticketDiscountRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.ticketDiscountRegistry.get(id)!;
    }

    details = async (id: string) => {
        return await agent.TicketDiscounts.getSellersOne(id);
    }

    createOne = async (ticketDiscount: TicketDiscount) => {
        this.loading = true;

        try {
            await agent.TicketDiscounts.createSellersOne(ticketDiscount);

            runInAction(() => {
                this.ticketDiscountRegistry.set(ticketDiscount.id!, ticketDiscount);
                this.selectedElement = ticketDiscount;
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

    editOne = async (ticketDiscount: TicketDiscount) => {
        this.loading = true;

        try {
            await agent.TicketDiscounts.editSellersOne(ticketDiscount);
            runInAction(() => {
                this.ticketDiscountRegistry.set(ticketDiscount.id!, ticketDiscount);
                this.selectedElement = ticketDiscount;
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
            await agent.TicketDiscounts.deleteSellersOne(id);
            runInAction(() => {
                this.ticketDiscountRegistry.delete(id);
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

export default TicketDiscountStore