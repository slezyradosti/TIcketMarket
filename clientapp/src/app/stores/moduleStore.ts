import { runInAction } from "mobx";
import { BaseModel } from "../models/BaseModel";

// eslint-disable-next-line @typescript-eslint/no-namespace
module ModuleStore {

    export function convertDateFromApi(entity: BaseModel) {
        runInAction(() => {
            entity.createdAt = new Date(entity.createdAt);
            entity.updatedAt = new Date(entity.updatedAt);
        });
    }
}

export default ModuleStore;