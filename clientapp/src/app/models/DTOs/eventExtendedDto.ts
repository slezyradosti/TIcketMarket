import { Event } from "../tables/event";

export interface EventExtendedDto extends Event {
    totalTickets: number,
    availableTickets: number,
    purchasedTickets: number,
}