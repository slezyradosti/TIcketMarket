import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import EventCategoryStore from "./catalogues/eventCategoryStore";
import EventTableStore from "./catalogues/eventTableStore";
import EventTypeStore from "./catalogues/eventTypeStore";
import TicketTypeStore from "./catalogues/ticketTypeStore";
import EventStore from "./tables/eventStore";
import OrderStore from "./tables/orderStore";
import TicketOrderStore from "./tables/ticketOrderStore";
import TicketStore from "./tables/ticketStore";
import TicketDiscountStore from "./tables/ticketDiscountStore";
import TableEventStore from "./tables/tableEventStore";

interface Store {
    commonStore: CommonStore;
    modalStore: ModalStore;
    eventCategoryStore: EventCategoryStore,
    eventTableStore: EventTableStore,
    eventTypeStore: EventTypeStore,
    ticketTypeStore: TicketTypeStore,
    eventStore: EventStore,
    orderStore: OrderStore,
    ticketOrderStore: TicketOrderStore,
    ticketStore: TicketStore,
    ticketDiscountStore: TicketDiscountStore,
    tableEventStore: TableEventStore
}

export const store: Store = {
    commonStore: new CommonStore(),
    modalStore: new ModalStore(),
    eventCategoryStore: new EventCategoryStore(),
    eventTableStore: new EventTableStore(),
    eventTypeStore: new EventTypeStore(),
    ticketTypeStore: new TicketTypeStore(),
    eventStore: new EventStore(),
    orderStore: new OrderStore(),
    ticketOrderStore: new TicketOrderStore(),
    ticketStore: new TicketStore(),
    ticketDiscountStore: new TicketDiscountStore(),
    tableEventStore: new TableEventStore()

}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}