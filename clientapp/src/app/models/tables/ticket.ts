import { BaseModel } from "../BaseModel";
import { TicketType } from "../catalogues/ticketType";
import { Event } from "./event";
import { TicketDiscount } from "./ticketDiscount";

export interface Ticket extends BaseModel {
    number: string;
    type: TicketType | null;
    typeId: string;
    event: Event | null;
    eventId: string;
    discount: TicketDiscount | null;
    discountId: string;
    isPurchased: boolean;
    finalPrice: number;
}