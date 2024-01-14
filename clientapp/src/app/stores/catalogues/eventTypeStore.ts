import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../api/agent";
import { EventType } from "../../models/catalogues/eventType";

class EventTypeStore {
    eventTypeRegistry = new Map<string, EventType>();
    selectedElement: EventType | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    get getArray() {
        return Array.from(this.eventTypeRegistry.values());
    }

    clearData = () => {
        this.eventTypeRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadList = async () => {
        this.clearData();

        try {
            const result = await agent.EventTypes.list();
            result.forEach(eventType => {
                this.eventTypeRegistry.set(eventType.id!, eventType);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.eventTypeRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.eventTypeRegistry.get(id)!;
    }

    details = async (id: string) => {
        return await agent.EventTypes.getOne(id);
    }

    createOne = async (eventType: EventType) => {
        this.loading = true;

        try {
            await agent.EventTypes.createOne(eventType);

            runInAction(() => {
                this.eventTypeRegistry.set(eventType.id!, eventType);
                this.selectedElement = eventType;
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

    editOne = async (eventType: EventType) => {
        this.loading = true;

        try {
            await agent.EventTypes.editOne(eventType);
            runInAction(() => {
                this.eventTypeRegistry.set(eventType.id!, eventType);
                this.selectedElement = eventType;
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
            await agent.EventTypes.deleteOne(id);
            runInAction(() => {
                this.eventTypeRegistry.delete(id);
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

export default EventTypeStore