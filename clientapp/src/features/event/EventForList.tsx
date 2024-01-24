import { observer } from "mobx-react";
import { GridColumn, GridRow, ItemDescription, ItemExtra, ItemHeader } from "semantic-ui-react";
import { Event } from "../../app/models/tables/event";
import { Link } from "react-router-dom";
import { EventExtendedDto } from "../../app/models/DTOs/eventExtendedDto";

interface Props {
    event: EventExtendedDto;
}

function EventForList({ event }: Props) {
    //const { ticketStore } = useStore();

    return (
        <>
            <GridRow key={event.id}>
                <GridColumn width={4}>
                    <ItemHeader as={Link} to={`/event/${event.id}`}> {event.title} </ItemHeader>
                </GridColumn>
                <GridColumn width={9}>
                    <ItemDescription>Description: {event.description}</ItemDescription>
                    <ItemExtra>
                        Tickets
                        <p>Total: {event.totalTickets}</p>
                        <p>Available: {event.availableTickets}</p>
                        <p>Purchased: {event.purchasedTickets}</p>
                    </ItemExtra>
                </GridColumn>
            </GridRow>
        </>
    );
}

export default observer(EventForList);
