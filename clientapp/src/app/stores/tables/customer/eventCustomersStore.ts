import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../../api/agent";
import ModuleStore from "../../moduleStore";
import { EventExtendedDto } from "../../../models/DTOs/eventExtendedDto";

class EventCustomersStore {
    eventRegistry = new Map<string, EventExtendedDto>();
    selectedElement: EventExtendedDto | undefined = undefined;
    detailsElement: EventExtendedDto | undefined = undefined;
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
}

export default EventCustomersStore