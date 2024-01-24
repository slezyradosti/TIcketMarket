import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../api/agent";
import { Event } from "../../models/tables/event";
import ModuleStore from "../moduleStore";

class EventStore {
    eventRegistry = new Map<string, Event>();
    selectedElement: Event | undefined = undefined;
    detailsElement: Event | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    get getArray() {
        return Array.from(this.eventRegistry.values());
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
            const result = await agent.Events.list();
            result.forEach(event => {
                ModuleStore.convertEntityDateFromApi(event);

                this.eventRegistry.set(event.id!, event);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    loadSellersList = async () => {
        this.clearData();

        try {
            const result = await agent.Events.sellersList();
            result.forEach(event => {
                ModuleStore.convertEntityDateFromApi(event);
                event.date = ModuleStore.convertDateFromApi(event.date);

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

    details = async (id: string) => {
        let event = await agent.Events.getOne(id);
        ModuleStore.convertEntityDateFromApi(event);
        event.date = ModuleStore.convertDateFromApi(event.date);

        this.detailsElement = event;
    }

    detailsSellers = async (id: string) => {
        let event = await agent.Events.getSellersOne(id);
        ModuleStore.convertEntityDateFromApi(event);
        event.date = ModuleStore.convertDateFromApi(event.date);

        this.detailsElement = event;
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
}

export default EventStore