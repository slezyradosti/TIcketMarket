import { observer } from "mobx-react";
import { GridColumn, GridRow, ItemDescription, ItemHeader } from "semantic-ui-react";
import { Ticket } from "../../../app/models/tables/ticket";

interface Props {
    ticket: Ticket;
}

function TicketForList({ ticket }: Props) {
    return (
        <>
            <GridRow key={ticket.id}>
                <GridColumn width={4}>
                    <ItemHeader>Number: {ticket.number}</ItemHeader>
                </GridColumn>
                <GridColumn width={9}>
                    <ItemDescription>Event: {ticket.event?.title}</ItemDescription>
                    <ItemDescription>Type: {ticket.type?.type}</ItemDescription>
                    <ItemDescription>Price: {ticket.finalPrice}</ItemDescription>
                    <ItemDescription>Bought at: {ticket.updatedAt.toLocaleDateString()}</ItemDescription>
                </GridColumn>
            </GridRow>
        </>
    );
}

export default observer(TicketForList);
