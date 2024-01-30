import { runInAction } from "mobx";
import { BaseModel } from "../models/BaseModel";

// eslint-disable-next-line @typescript-eslint/no-namespace
module ModuleStore {

    export function convertEntityDateFromApi(entity: BaseModel) {
        if (entity.createdAt != null && entity.updatedAt != null) {
            runInAction(() => {
                entity.createdAt = new Date(entity.createdAt!);
                entity.updatedAt = new Date(entity.updatedAt!);
            });
        }
    }

    export function convertDateFromApi(apiDate: Date) {
        return new Date(apiDate);
    }
}

export default ModuleStore;