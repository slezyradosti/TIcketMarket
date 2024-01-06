import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../api/agent";
import { TicketType } from "../../models/catalogues/ticketType";

class TicketOrderStore {
    ticketTypeRegistry = new Map<string, TicketType>();
    selectedElement: TicketType | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    clearData = () => {
        this.ticketTypeRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadSellersList = async () => {
        try {
            const result = await agent.TicketTypes.getSellersList();
            result.forEach(ticketType => {
                this.ticketTypeRegistry.set(ticketType.id!, ticketType);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.ticketTypeRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.ticketTypeRegistry.get(id)!;
    }

    details = async (id: string) => {
        return await agent.TicketTypes.getSellersOne(id);
    }

    createOne = async (ticketType: TicketType) => {
        this.loading = true;

        try {
            await agent.TicketTypes.createSellersOne(ticketType);

            runInAction(() => {
                this.ticketTypeRegistry.set(ticketType.id!, ticketType);
                this.selectedElement = ticketType;
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

    editOne = async (ticketType: TicketType) => {
        this.loading = true;

        try {
            await agent.TicketTypes.editOne(ticketType);
            runInAction(() => {
                this.ticketTypeRegistry.set(ticketType.id!, ticketType);
                this.selectedElement = ticketType;
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
            await agent.TicketTypes.deleteSellersOne(id);
            runInAction(() => {
                this.ticketTypeRegistry.delete(id);
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