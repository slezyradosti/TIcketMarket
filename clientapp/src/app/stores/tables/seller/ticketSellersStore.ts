import { makeAutoObservable, runInAction } from "mobx";
import { Ticket } from "../../../models/tables/ticket";
import { EventTicketsAmountDto } from "../../../models/DTOs/eventTicketsAmountDto";
import agent from "../../../api/agent";
import ModuleStore from "../../moduleStore";

class TicketSellersStore {
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
  };

  setLoadingInitial = (state: boolean) => {
    this.loadingInitial = state;
  };

  loadList = async (eventId: string) => {
    this.clearData();

    try {
      const result = await agent.Tickets.detailedTicketList(eventId);
      result.forEach((ticket) => {
        ModuleStore.convertEntityDateFromApi(ticket);

        this.ticketRegistry.set(ticket.id!, ticket);
      });
    } catch (error) {
      console.log(error);
    } finally {
      this.setLoadingInitial(false);
    }
  };

  selectOne = (id: string) => {
    this.selectedElement = this.ticketRegistry.get(id);
  };

  cancelSelectedElement = () => {
    this.selectedElement = undefined;
  };

  getOne = (id: string) => {
    return this.ticketRegistry.get(id)!;
  };

  details = async (id: string) => {
    this.detailsElement = await agent.Tickets.getCustomersOne(id);
  };

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
  };

  deleteOne = async (id: string) => {
    this.loading = true;

    try {
      await agent.Tickets.deleteOne(id);
      runInAction(() => {
        this.ticketRegistry.delete(id);
        if (this.selectedElement?.id === id) this.cancelSelectedElement();
      });
    } catch (error) {
      console.log(error);
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  };

  getEventTicketAmount = async (eventId: string) => {
    this.loading = true;

    try {
      const evTicketAmount = await agent.Tickets.getEventTicketAmount(eventId);
      runInAction(() => (this.eventTicketAmount = evTicketAmount));
    } catch (error) {
      console.log(error);
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  };

  generateTickets = async (ticket: Ticket, amount: number) => {
    this.loading = true;

    try {
      await agent.Tickets.generateTickets(ticket, amount);
      await this.getEventTicketAmount(ticket.eventId);
    } catch (error) {
      console.log(error);
    } finally {
      runInAction(() => {
        this.loading = false;
        this.editMode = false;
      });
    }
  };
}

export default TicketSellersStore;
