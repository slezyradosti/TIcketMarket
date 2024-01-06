import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import ModalStore from "./modalStore";
import EventCategoryStore from "./catalogues/eventCategoryStore";
import EventTableStore from "./catalogues/eventTableStore";
import EventTypeStore from "./catalogues/eventTypeStore";
import TicketTypeStore from "./catalogues/ticketTypeStore";

interface Store {
    commonStore: CommonStore;
    modalStore: ModalStore;
    eventCategoryStore: EventCategoryStore,
    eventTableStore: EventTableStore,
    eventTypeStore: EventTypeStore,
    ticketTypeStore: TicketTypeStore,
}

export const store: Store = {
    commonStore: new CommonStore(),
    modalStore: new ModalStore(),
    eventCategoryStore: new EventCategoryStore(),
    eventTableStore: new EventTableStore(),
    eventTypeStore: new EventTypeStore(),
    ticketTypeStore: new TicketTypeStore(),

}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}