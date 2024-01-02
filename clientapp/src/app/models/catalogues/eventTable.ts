import { BaseModel } from "../BaseModel";

export interface EventTable extends BaseModel {
    number: string;
    price: number;
    peopleQuantity: number;
}