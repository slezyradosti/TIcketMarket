import { observer } from "mobx-react";
import { TableRow, TableCell, Button, Icon } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { Link } from "react-router-dom";
import { Ticket } from "../../../app/models/tables/ticket";

interface Props {
  ticket: Ticket;
}

function TicketForList({ ticket }: Props) {
  const { ticketSellersStore } = useStore();
  const { loading, deleteOne } = ticketSellersStore;

  const handleDelete = (tEventId: string) => {
    deleteOne(tEventId);
  };

  return (
    <>
      <TableRow>
        <TableCell>
          {" "}
          <a href={`#`}>{ticket?.number}</a>{" "}
        </TableCell>
        <TableCell>{ticket?.type?.type}</TableCell>
        <TableCell>{ticket?.discount?.discountPercentage}</TableCell>
        <TableCell>{ticket?.isPurchased}</TableCell>
        <TableCell>
          <Button as={Link} to={`manage/${ticket?.id}`}>
            Edit
          </Button>
        </TableCell>
        <TableCell>
          <Button
            icon
            onClick={() => handleDelete(ticket!.id)}
            loading={loading}
          >
            <Icon name="trash alternate outline" />
          </Button>
        </TableCell>
      </TableRow>
    </>
  );
}

export default observer(TicketForList);
