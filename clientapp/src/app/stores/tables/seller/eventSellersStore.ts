import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../../api/agent";
import ModuleStore from "../../moduleStore";
import { Event } from "../../../models/tables/event";

class EventSellersStore {
    eventRegistry = new Map<string, Event>();
    selectedElement: Event | undefined = undefined;
    detailsElement: Event | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;
    eventOptions: { key: string; text: string; value: string; }[] = [];
    eventOptionsLoading: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    get getArray() {
        return Array.from(this.eventRegistry.values());
    }

    loadOptions = async () => {
        this.eventOptionsLoading = true;
        this.eventOptions = [];

        if (this.getArray.length <= 0) {
            await this.loadList();
        }

        this.getArray.forEach(event => {
            const opt = { key: event.id, text: event.title, value: event.id }
            this.eventOptions?.push(opt);
        })

        this.eventOptionsLoading = false;
    }

    clearData = () => {
        this.eventRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadList = async () => {
        this.clearData();

        try {
            const result = await agent.Events.sellersList();
            result.forEach(event => {
                ModuleStore.convertEntityDateFromApi(event);
                event.date = ModuleStore.convertDateFromApi(event.date!);

                this.eventRegistry.set(event.id!, event);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.eventRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.eventRegistry.get(id)!;
    }

    detailsSellers = async (id: string) => {
        let event = await agent.Events.getSellersOne(id);
        ModuleStore.convertEntityDateFromApi(event);
        event.date = ModuleStore.convertDateFromApi(event.date!);

        this.detailsElement = event;

        return event;
    }

    createOne = async (event: Event) => {
        this.loading = true;

        try {
            await agent.Events.createOne(event);

            runInAction(() => {
                this.eventRegistry.set(event.id!, event);
                this.selectedElement = event;
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

    editOne = async (event: Event) => {
        this.loading = true;

        try {
            await agent.Events.editOne(event);
            runInAction(() => {
                this.eventRegistry.set(event.id!, event);
                this.selectedElement = event;
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
            await agent.Events.deleteOne(id);
            runInAction(() => {
                this.eventRegistry.delete(id);
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

    openForm = (id?: string) => {
        id ? this.selectOne(id) : this.cancelSelectedElement();
        this.editMode = true;
    }

    closeForm = () => {
        this.editMode = false;
    }
}

export default EventSellersStore