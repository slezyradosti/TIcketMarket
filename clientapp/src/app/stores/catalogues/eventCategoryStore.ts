import { makeAutoObservable } from "mobx";
import { EventCategory } from "../../models/catalogues/eventCategory";
import agent from "../../api/agent";

class EcentCategoryStore {
    eventCategoryRegistry = new Map<string, EventCategory>();
    selectedElement: EventCategory | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    clearData = () => {
        this.eventCategoryRegistry.clear();
        this.loadingInitial = true;
    }

    loadList = async () => {
        try {
            const result = await agent.
        } catch (error) {

        }
    }
}