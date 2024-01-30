import { observer } from "mobx-react";
import { TableRow, TableCell, Button, Icon } from "semantic-ui-react";
import { Event } from "../../../app/models/tables/event";
import { useStore } from "../../../app/stores/store";

interface Props {
    event: Event;
}

function EventForSellersList({ event }: Props) {
    const { eventSellersStore } = useStore();
    const { loading, deleteOne } = eventSellersStore

    const handleDelete = (eventId: string) => {
        deleteOne(eventId);
    }

    return (
        <>
            <TableRow>
                <TableCell> <a href={`/event/my-events/${event.id}`}>{event.title}</a> </TableCell>
                <TableCell>{event.date?.toLocaleDateString()}</TableCell>
                <TableCell>{event.createdAt?.toLocaleDateString()}</TableCell>
                <TableCell>{event.place}</TableCell>
                <TableCell>{event.type?.type} a</TableCell>
                <TableCell><Button>Edit</Button></TableCell>
                <TableCell>
                    <Button
                        icon
                        onClick={() => handleDelete(event.id)}
                        loading={loading}
                    >
                        <Icon name="trash alternate outline" />
                    </Button>
                </TableCell>
            </TableRow>
        </>
    );
}

export default observer(EventForSellersList);
