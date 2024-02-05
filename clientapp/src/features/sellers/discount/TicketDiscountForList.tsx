import { observer } from "mobx-react";
import { Button, Icon, TableCell, TableRow } from "semantic-ui-react";
import { TicketDiscount } from "../../../app/models/tables/ticketDiscount";
import { Link } from "react-router-dom";
import { useStore } from "../../../app/stores/store";

interface Props {
    ticketDiscount: TicketDiscount;
}

function TicketDiscountForList({ ticketDiscount }: Props) {
    const { ticketDiscountStore } = useStore();
    const { loading, deleteOne } = ticketDiscountStore

    const handleDelete = (eventId: string) => {
        deleteOne(eventId);
    }

    return (
        <>
            <TableRow>
                <TableCell>Percentage: {ticketDiscount.discountPercentage} %</TableCell>
                <TableCell>Created at: {ticketDiscount.createdAt?.toLocaleDateString()}</TableCell>
                <TableCell>{ticketDiscount.isActivated ? 'Activated' : 'Not activated'} </TableCell>
                <TableCell>
                    <Button
                        as={Link} to={`manage/${ticketDiscount.id}`}
                    >
                        Edit
                    </Button>
                </TableCell>
                <TableCell>
                    <Button
                        icon
                        onClick={() => handleDelete(ticketDiscount.id)}
                        loading={loading}
                    >
                        <Icon name="trash alternate outline" />
                    </Button>
                </TableCell>
            </TableRow>
        </>
    );
}

export default observer(TicketDiscountForList);
