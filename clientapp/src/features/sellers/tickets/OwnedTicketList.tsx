import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import {
  Button,
  Dropdown,
  Icon,
  Table,
  TableBody,
  TableFooter,
  TableHeader,
  TableHeaderCell,
  TableRow,
} from "semantic-ui-react";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import TicketForList from "./TicketForList";

function OwnedTicketList() {
  const { eventSellersStore, ticketSellersStore } = useStore();
  const [chosenEvent, setChosenEvent] = useState<string | null>(null);

  useEffect(() => {
    if (chosenEvent == null) {
      ticketSellersStore.clearData();
      eventSellersStore.loadOptions();
    } else if (chosenEvent) {
      ticketSellersStore.loadList(chosenEvent);
    }
  }, [ticketSellersStore, chosenEvent]);

  const handleEventSelectChange = (event, data) => {
    setChosenEvent(data.value);
  };

  if (eventSellersStore.eventOptionsLoading) {
    return <LoadingComponent content="Loading page..." />;
  }

  return (
    <>
      <div className="ui thingy">
        <Dropdown
          placeholder="Event"
          search
          selection
          options={eventSellersStore.eventOptions}
          onChange={(e, data) => handleEventSelectChange(e, data)}
        />
      </div>

      {chosenEvent && ticketSellersStore.loadingInitial ? (
        <>
          return <LoadingComponent content="Loading page..." />;
        </>
      ) : (
        <>
          <Table compact celled>
            <TableHeader>
              <TableRow>
                <TableHeaderCell>Ticket number</TableHeaderCell>
                <TableHeaderCell>Ticket type</TableHeaderCell>
                <TableHeaderCell>Ticket discount</TableHeaderCell>
                <TableHeaderCell>Ticket purchased</TableHeaderCell>
                <TableHeaderCell>Edit</TableHeaderCell>
                <TableHeaderCell>Delete</TableHeaderCell>
              </TableRow>
            </TableHeader>

            <TableBody>
              {ticketSellersStore.getArray.map((ticket) => (
                <>
                  <TicketForList ticket={ticket} />
                </>
              ))}
            </TableBody>

            <TableFooter fullWidth>
              <TableRow>
                <TableHeaderCell colSpan="6">
                  <Button
                    floated="right"
                    icon="add"
                    labelPosition="left"
                    primary
                    size="small"
                    as={Link}
                    to="manage"
                  >
                    <Icon name="add" /> Add new
                  </Button>
                </TableHeaderCell>
              </TableRow>
            </TableFooter>
          </Table>
        </>
      )}
    </>
  );
}

export default observer(OwnedTicketList);
