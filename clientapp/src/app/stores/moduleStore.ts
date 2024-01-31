import { runInAction } from "mobx";
import { BaseModel } from "../models/BaseModel";

// eslint-disable-next-line @typescript-eslint/no-namespace
module ModuleStore {

    export function convertEntityDateFromApi(entity: BaseModel) {
        runInAction(() => {
            entity.createdAt = entity.createdAt != null ? new Date(entity.createdAt!) : entity.createdAt;
            entity.updatedAt = entity.updatedAt != null ? new Date(entity.updatedAt!) : entity.updatedAt;
        });
    }

    export function convertDateFromApi(apiDate: Date) {
        return new Date(apiDate);
    }
}

export default ModuleStore;