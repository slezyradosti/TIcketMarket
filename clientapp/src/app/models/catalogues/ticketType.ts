import { BaseModel } from "../BaseModel";

export interface TicketType extends BaseModel {
    type: string;
    price: number;
}