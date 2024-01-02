import { BaseModel } from "../BaseModel";
import { Order } from "./order";
import { Ticket } from "./ticket";

export interface TicketOrder extends BaseModel {
    order: Order | null;
    orderId: string | null;
    ticket: Ticket | null;
    ticketId: string | null;
}