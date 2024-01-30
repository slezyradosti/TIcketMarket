import { observer } from "mobx-react";
import { Button, TableCell, TableRow } from "semantic-ui-react";
import { TicketDiscount } from "../../../app/models/tables/ticketDiscount";

interface Props {
    ticketDiscount: TicketDiscount;
}

function TicketDiscountForList({ ticketDiscount }: Props) {
    return (
        <>
            <TableRow>
                <TableCell>Percentage: {ticketDiscount.discountPercentage} %</TableCell>
                <TableCell>Created at: {ticketDiscount.createdAt?.toLocaleDateString()}</TableCell>
                <TableCell>{ticketDiscount.isActivated ? 'Activated' : 'Not activated'} </TableCell>
                <TableCell><Button>Edit</Button></TableCell>
                <TableCell><Button>Delete</Button></TableCell>
            </TableRow>
        </>
    );
}

export default observer(TicketDiscountForList);
