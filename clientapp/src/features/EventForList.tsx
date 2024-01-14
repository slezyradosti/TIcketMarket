import { observer } from "mobx-react";
import { GridColumn, GridRow, ItemDescription, ItemHeader } from "semantic-ui-react";
import { Event } from "../app/models/tables/event";

interface Props {
    event: Event;
}

function EventForList({ event }: Props) {
    return (
        <>
            <GridRow key={event.id}>
                <GridColumn width={4}>
                    <ItemHeader> {event.title}</ItemHeader>
                </GridColumn>
                <GridColumn width={9}>
                    <ItemDescription>{event.description}</ItemDescription>
                </GridColumn>
            </GridRow>
        </>
    );
}

export default observer(EventForList);
