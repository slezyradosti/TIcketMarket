import { observer } from "mobx-react";
import { Link } from "react-router-dom";
import { TableRow, TableCell, Button } from "semantic-ui-react";
import { Event } from "../../../app/models/tables/event";

interface Props {
    event: Event;
}

function EventForSellersList({ event }: Props) {

    return (
        <>
            <TableRow>
                <TableCell> <a href={`/event/my-events/${event.id}`}>{event.title}</a> </TableCell>
                <TableCell>{event.date.toLocaleDateString()}</TableCell>
                <TableCell>{event.createdAt.toLocaleDateString()}</TableCell>
                <TableCell>{event.place}</TableCell>
                <TableCell>{event.type?.type} a</TableCell>
                <TableCell><Button>Edit</Button></TableCell>
                <TableCell><Button>Delete</Button></TableCell>
            </TableRow>
        </>
    );
}

export default observer(EventForSellersList);
