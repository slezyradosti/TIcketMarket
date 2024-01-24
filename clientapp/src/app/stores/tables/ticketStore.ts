import { makeAutoObservable, runInAction } from "mobx";
import agent from "../../api/agent";
import { Ticket } from "../../models/tables/ticket";
import { EventTicketsAmountDto } from "../../models/DTOs/eventTicketsAmountDto";
import { ApplyDiscountDto } from "../../models/DTOs/applyDiscountDto";
import ModuleStore from "../moduleStore";

class TicketStore {
    ticketRegistry = new Map<string, Ticket>();
    selectedElement: Ticket | undefined = undefined;
    detailsElement: Ticket | undefined = undefined;
    editMode: boolean = false;
    loading: boolean = false;
    loadingInitial: boolean = true;
    eventTicketAmount: EventTicketsAmountDto | undefined = undefined;

    constructor() {
        makeAutoObservable(this);
    }

    get getArray() {
        return Array.from(this.ticketRegistry.values());
    }

    clearData = () => {
        this.ticketRegistry.clear();
        this.loadingInitial = true;
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    // loadCustomersList = async () => {
    //     this.clearData();

    //     try {
    //         const result = await agent.Tickets.customersList();
    //         result.forEach(ticket => {
    //             ModuleStore.convertDateFromApi(ticket);

    //             this.ticketRegistry.set(ticket.id!, ticket);
    //         })
    //     } catch (error) {
    //         console.log(error);
    //     } finally {
    //         this.setLoadingInitial(false);
    //     }
    // }

    loadAvailableList = async (eventId: string) => {
        this.clearData();

        try {
            const result = await agent.Tickets.availableTicketList(eventId);
            result.forEach(ticket => {
                ModuleStore.convertEntityDateFromApi(ticket);

                this.ticketRegistry.set(ticket.id!, ticket);
            })
        } catch (error) {
            console.log(error);
        } finally {
            this.setLoadingInitial(false);
        }
    }

    selectOne = (id: string) => {
        this.selectedElement = this.ticketRegistry.get(id);
    }

    cancelSelectedElement = () => {
        this.selectedElement = undefined;
    }

    getOne = (id: string) => {
        return this.ticketRegistry.get(id)!;
    }

    detailsCustomers = async (id: string) => {
        this.detailsElement = await agent.Tickets.getCustomersOne(id);
    }

    createOne = async (ticket: Ticket) => {
        this.loading = true;

        try {
            await agent.Tickets.createOne(ticket);

            runInAction(() => {
                this.ticketRegistry.set(ticket.id!, ticket);
                this.selectedElement = ticket;
            });
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }

    deleteOne = async (id: string) => {
        this.loading = true;

        try {
            await agent.Tickets.deleteOne(id);
            runInAction(() => {
                this.ticketRegistry.delete(id);
                if (this.selectedElement?.id === id) this.cancelSelectedElement();
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    generateTickets = async (ticket: Ticket, amount: number) => {
        this.loading = true;

        try {
            await agent.Tickets.generateTickets(ticket, amount);

            // TODO loading?
            await this.getEventTicketAmount(ticket.eventId);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }

    getEventTicketAmount = async (eventId: string) => {
        this.loading = true;

        try {
            const evTicketAmount = await agent.Tickets.getEventTicketAmount(eventId);
            runInAction(() => this.eventTicketAmount = evTicketAmount);
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    applyDiscount = async (applyDiscountDto: ApplyDiscountDto) => {
        this.loading = true;

        try {
            await agent.Tickets.applyDiscount(applyDiscountDto);

        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }

    removeDiscount = async (ticketId: string) => {
        this.loading = true;

        try {
            await agent.Tickets.removeDiscount(ticketId);

        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }

    getTicketToBuy = async (eventId: string, ticketTypeId: string) => {
        this.loading = true;

        try {
            this.selectedElement = await agent.Tickets.getTicketToBuy(eventId, ticketTypeId);
            // Do I need return?
            return this.selectedElement;
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
            });
        }
    }

    purchaseTicket = async (ticketId: string) => {
        this.loading = true;

        try {
            await agent.Tickets.purchaseTicket(ticketId);
            // TODO any other actions?

        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loading = false;
                this.editMode = false;
            });
        }
    }
}

export default TicketStore