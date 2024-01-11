import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../api/agent";
import { TableEvent } from "../../models/tables/tableEvent";

class TableEventStore {
    tableEventRegistry = new Map<string, TableEvent>();
    selectedElement: TableEvent | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    clearData = () => {
        this.tableEventRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    loadList = async (eventId: string) => {
        try {
            const result = await agent.TableEvents.list(eventId);
            result.forEach(tableEvent => {
                this.tableEventRegistry.set(tableEvent.id!, tableEvent);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.tableEventRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.tableEventRegistry.get(id)!;
    }

    details = async (id: string) => {
        return await agent.TableEvents.getOne(id);
    }

    createOne = async (tableEvent: TableEvent) => {
        this.loading = true;

        try {
            await agent.TableEvents.createOne(tableEvent);

            runInAction(() => {
                this.tableEventRegistry.set(tableEvent.id!, tableEvent);
                this.selectedElement = tableEvent;
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

    editOne = async (tableEvent: TableEvent) => {
        this.loading = true;

        try {
            await agent.TableEvents.editOne(tableEvent);
            runInAction(() => {
                this.tableEventRegistry.set(tableEvent.id!, tableEvent);
                this.selectedElement = tableEvent;
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
            await agent.TableEvents.deleteOne(id);
            runInAction(() => {
                this.tableEventRegistry.delete(id);
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

export default TableEventStore