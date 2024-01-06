import axios, { AxiosError, AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { store } from '../stores/store';
import { router } from '../router/routes';
import { Event } from '../models/tables/event';
import { EventCategory } from '../models/catalogues/eventCategory';
import { EventTable } from '../models/catalogues/eventTable';
import { EventType } from '../models/catalogues/eventType';
import { Order } from '../models/tables/order';
import { TableEvent } from '../models/tables/tableEvent';
import { Ticket } from '../models/tables/ticket';
import { EventTicketsAmountDto } from '../models/DTOs/EventTicketsAmountDto';
import { ApplyDiscountDto } from '../models/DTOs/applyDiscountDto';
import { TicketDiscount } from '../models/tables/ticketDiscount';
import { TicketOrder } from '../models/tables/ticketOrder';
import { TicketType } from '../models/catalogues/ticketType';

axios.defaults.baseURL = 'https://localhost:7177';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    });
}

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;

    return config;
})

axios.interceptors.response.use(async response => {
    if (import.meta.env.DEV) await sleep(1000);

    // TODO pagination
    const pagination = response.headers['pagination'];
}, (error: AxiosError) => {
    const { data, status, config } = error.response as AxiosResponse;

    switch (status) {
        case 400:
            if (config.method === 'get' && data.errors.hasOwnPropery('id')) {
                router.navigate('/not-found');
            }
            if (data.error) {
                const modalStateErrors = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateErrors.push(data.errors[key]);
                    }
                }

                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('unauthorised');
            break;
        case 403:
            toast.error('forbidden');
            break;
        case 404:
            router.navigate('/not-found');
            break;
        case 500:
            store.commonStore.setServerError(data);
            router.navigate('/server-error');
            break;
    }
    return Promise.reject(error);
})


const responseBody = <T>(reponse: AxiosResponse<T>) => reponse.data; // get responseData from http response

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: object) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Events = {
    sellersList: () => requests.get<Event[]>('/event/my-events'),
    list: () => requests.get<Event[]>('/event/list'),
    getSellersOne: (id: string) => requests.get<Event>(`/event/${id}`),
    createOne: (event: Event) => requests.post<void>('/event', event),
    editOne: (event: Event) => requests.put<void>(`/event/${event.id}`, event),
    deleteOne: (id: string) => requests.delete<void>(`/event/${id}`)
}

const EventCategories = {
    list: () => requests.get<EventCategory[]>('/EventCategory/list'),
    getSellersOne: (id: string) => requests.get<EventCategory>(`/EventCategory/${id}`),
    createOne: (eventCategory: EventCategory) => requests.post<void>('/EventCategory', eventCategory),
    editOne: (eventCategory: EventCategory) => requests.put<void>(`/EventCategory/${eventCategory.id}`, eventCategory),
    deleteOne: (id: string) => requests.delete<void>(`/EventCategory/${id}`)
}

const EventTables = {
    list: () => requests.get<EventTable[]>('/EventTable/list'),
    getOne: (id: string) => requests.get<EventTable>(`/EventTable/${id}`),
    createOne: (eventTable: EventTable) => requests.post<void>('/EventTable', eventTable),
    editOne: (eventTable: EventTable) => requests.put<void>(`/EventTable/${eventTable.id}`, eventTable),
    deleteOne: (id: string) => requests.delete<void>(`/EventTable/${id}`)
}

const EventTypes = {
    list: () => requests.get<EventType[]>('/EventType/list'),
    getOne: (id: string) => requests.get<EventType>(`/EventType/${id}`),
    createOne: (eventType: EventType) => requests.post<void>('/EventType', eventType),
    editOne: (eventType: EventType) => requests.put<void>(`/EventType/${eventType.id}`, eventType),
    deleteOne: (id: string) => requests.delete<void>(`/EventType/${id}`)
}

const Orders = {
    customersOrderlist: () => requests.get<Order[]>('/Order/my-orders'),
    getCustomersOne: (id: string) => requests.get<Order>(`/Order/${id}`),
    createOne: (order: Order) => requests.post<void>('/Order', order),
    //editOne: (order: Order) => requests.put<void>(`/Order/${order.id}`, order),
    deleteOne: (id: string) => requests.delete<void>(`/Order/${id}`)
}

const TableEvents = {
    list: (eventId: string) => requests.get<TableEvent[]>(`/TableEvent/list/${eventId}`),
    getOne: (id: string) => requests.get<TableEvent>(`/TableEvent/${id}`),
    createOne: (tableEvent: TableEvent) => requests.post<void>('/TableEvent', tableEvent),
    editOne: (tableEvent: TableEvent) => requests.put<void>(`/TableEvent/${tableEvent.id}`, tableEvent),
    deleteOne: (id: string) => requests.delete<void>(`/TableEvent/${id}`)
}

const Tickets = {
    customersList: () => requests.get<Ticket[]>('/Ticket/my-ticket'),
    availableTicketList: (eventId: string) => requests.get<Ticket[]>(`/Ticket/available-tickets/${eventId}`),
    getCustomersOne: (id: string) => requests.get<Ticket>(`/Ticket/${id}`),
    createOne: (ticket: Ticket) => requests.post<void>('/Ticket', ticket),
    //editOne: (ticket: Ticket) => requests.put<void>(`/Ticket/${ticket.id}`, ticket),
    deleteOne: (id: string) => requests.delete<void>(`/Ticket/${id}`),
    generateTickets: (ticket: Ticket, amount: number) => requests.post<void>(`/Ticket/generate-tickets?ticket-amount=${amount}`, ticket),
    getEventTicketAmount: (eventId: string) => requests.get<EventTicketsAmountDto>(`/Ticket/event-ticket-amount/${eventId}`),
    applyDiscount: (applyDiscountDto: ApplyDiscountDto) => requests.put<void>('/Ticket/apply-discount', applyDiscountDto),
    removeDiscount: (ticketId: string) => requests.put<void>(`/Ticket/remove-discount/${ticketId}`, {}),
    getTicketToBuy: (eventId: string, ticketTypeId: string) => requests
        .get<Ticket>(`/Ticket/ticket-to-buy?event-id=${eventId}&ticket-type-id=${ticketTypeId}`),
    purchaseTicket: (ticketId: string) => requests.post<void>(`/Ticket/purchase-ticket/${ticketId}`, {}),
}

const TicketDiscounts = {
    getSellersList: () => requests.get<TicketDiscount[]>(`/TicketDiscount/my-discounts`),
    getSellersOne: (id: string) => requests.get<TicketDiscount>(`/TicketDiscount/${id}`),
    createSellersOne: (ticketDiscount: TicketDiscount) => requests.post<void>('/TicketDiscount', ticketDiscount),
    editSellersOne: (ticketDiscount: TicketDiscount) => requests.put<void>(`/TicketDiscount/${ticketDiscount.id}`, ticketDiscount),
    deleteSellersOne: (id: string) => requests.delete<void>(`/TicketDiscount/${id}`)
}

const TicketOrders = {
    getCustomersList: () => requests.get<TicketOrder[]>(`/TicketOrder/my-ticket-orders`),
    getCustomersOne: (id: string) => requests.get<TicketOrder>(`/TicketOrder/${id}`),
    createOne: (ticketOrder: TicketOrder) => requests.post<void>('/TicketOrder', ticketOrder),
    editOne: (ticketOrder: TicketOrder) => requests.put<void>(`/TicketOrder/${ticketOrder.id}`, ticketOrder),
    deleteOne: (id: string) => requests.delete<void>(`/TicketOrder/${id}`)
}

const TicketTypes = {
    getSellersList: () => requests.get<TicketType[]>(`/TicketType/list`),
    getSellersOne: (id: string) => requests.get<TicketType>(`/TicketType/${id}`),
    createSellersOne: (ticketType: TicketType) => requests.post<void>('/TicketType', ticketType),
    editOne: (ticketType: TicketType) => requests.put<void>(`/TicketType/${ticketType.id}`, ticketType),
    deleteSellersOne: (id: string) => requests.delete<void>(`/TicketType/${id}`)
}

const agent = {
    Events,
    EventCategories,
    EventTables,
    EventTypes,
    Orders,
    TableEvents,
    Tickets,
    TicketDiscounts,
    TicketOrders,
    TicketTypes,
}

export default agent;