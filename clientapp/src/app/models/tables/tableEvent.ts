import { BaseModel } from "../BaseModel";
import { EventTable } from "../catalogues/eventTable";
import { Event } from "./event";

export interface TableEvent extends BaseModel {
    event: Event | null;
    eventId: string;
    table: EventTable | null;
    tableId: string;
}