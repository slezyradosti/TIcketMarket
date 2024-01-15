import { makeAutoObservable, runInAction } from "mobx";
import { EventTable } from "../../models/catalogues/eventTable";
import agent from "../../api/agent";
import ModuleStore from "../moduleStore";

class EventTableStore {
    eventTableRegistry = new Map<string, EventTable>();
    selectedElement: EventTable | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    clearData = () => {
        this.eventTableRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    get getArray() {
        return Array.from(this.eventTableRegistry.values());
    }

    loadList = async () => {
        this.clearData();

        try {
            const result = await agent.EventTables.list();
            result.forEach(eventTable => {
                ModuleStore.convertDateFromApi(eventTable);

                this.eventTableRegistry.set(eventTable.id!, eventTable);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.eventTableRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.eventTableRegistry.get(id)!;
    }

    details = async (id: string) => {
        return await agent.EventTables.getOne(id);
    }

    createOne = async (eventTable: EventTable) => {
        this.loading = true;

        try {
            await agent.EventTables.createOne(eventTable);

            runInAction(() => {
                this.eventTableRegistry.set(eventTable.id!, eventTable);
                this.selectedElement = eventTable;
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

    editOne = async (eventTable: EventTable) => {
        this.loading = true;

        try {
            await agent.EventTables.editOne(eventTable);
            runInAction(() => {
                this.eventTableRegistry.set(eventTable.id!, eventTable);
                this.selectedElement = eventTable;
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
            await agent.EventTables.deleteOne(id);
            runInAction(() => {
                this.eventTableRegistry.delete(id);
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

export default EventTableStore