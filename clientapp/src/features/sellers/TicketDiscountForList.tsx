import { observer } from "mobx-react";
import { Checkbox, GridColumn, GridRow, ItemExtra, ItemHeader, ItemMeta } from "semantic-ui-react";
import { TicketDiscount } from "../../app/models/tables/ticketDiscount";

interface Props {
    ticketDiscount: TicketDiscount;
}

function TicketDiscountForList({ ticketDiscount }: Props) {
    return (
        <>
            {/* <GridRow key={ticketDiscount.id}> */}

            <GridColumn width={5} key={ticketDiscount.id}>
                <ItemHeader>Percentage: {ticketDiscount.discountPercentage} %</ItemHeader>
                <ItemMeta>Created at: {ticketDiscount.createdAt.toLocaleDateString()}</ItemMeta>
                <ItemExtra> {ticketDiscount.isActivated ? 'Activated' : 'Not activated'} </ItemExtra>
            </GridColumn>
            {/* </GridRow> */}
        </>
    );
}

export default observer(TicketDiscountForList);
