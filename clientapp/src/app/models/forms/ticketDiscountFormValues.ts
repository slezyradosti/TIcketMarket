export class TicketDiscountFormValues {
    id: string | null = null;
    discountPercentage: number = 0;
    code: string = '';
    isActivated: boolean = false;

    constructor(ticketDiscount?: TicketDiscountFormValues) {
        if (ticketDiscount) {
            this.discountPercentage = ticketDiscount.discountPercentage
            this.code = ticketDiscount.code
            this.isActivated = ticketDiscount.isActivated
        }
    }
}