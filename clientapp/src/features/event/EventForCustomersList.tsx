import { observer } from "mobx-react";
import { GridColumn, GridRow, ItemDescription, ItemHeader, Statistic, StatisticGroup, StatisticLabel, StatisticValue } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { EventExtendedDto } from "../../app/models/DTOs/eventExtendedDto";

interface Props {
    event: EventExtendedDto;
}

function EventForCustomersList({ event }: Props) {

    return (
        <>
            <GridRow key={event.id}>
                <GridColumn width={4}>
                    <ItemHeader as={Link} to={`/event/${event.id}`}> {event.title} </ItemHeader>
                </GridColumn>
                <GridColumn width={9}>
                    <ItemDescription>Description: {event.description}</ItemDescription>
                    <StatisticGroup size='mini'>
                        <Statistic>
                            <StatisticValue>{event.totalPlaces}</StatisticValue>
                            <StatisticLabel>Total places:</StatisticLabel>
                        </Statistic>
                        <Statistic>
                            <StatisticValue>{event.availableTickets}</StatisticValue>
                            <StatisticLabel>Tickets available</StatisticLabel>
                        </Statistic>
                        <Statistic>
                            <StatisticValue>{event.purchasedTickets}</StatisticValue>
                            <StatisticLabel>TIckets purchased</StatisticLabel>
                        </Statistic>
                    </StatisticGroup>
                </GridColumn>
            </GridRow>
        </>
    );
}

export default observer(EventForCustomersList);
