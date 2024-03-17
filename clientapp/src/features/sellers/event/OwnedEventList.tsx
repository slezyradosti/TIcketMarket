import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import {
  Button,
  Icon,
  Table,
  TableBody,
  TableFooter,
  TableHeader,
  TableHeaderCell,
  TableRow,
} from "semantic-ui-react";
import { useEffect } from "react";
import EventForSellersList from "./EventForSellersList";
import { Link } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";

function OwnedEventList() {
  const { eventSellersStore } = useStore();
  const { getArray } = eventSellersStore;

  useEffect(() => {
    eventSellersStore.loadList();
  }, [eventSellersStore]);

  if (eventSellersStore.loadingInitial) {
    return <LoadingComponent content="Loading page..." />;
  }

  return (
    <>
      <Table compact celled>
        <TableHeader>
          <TableRow>
            <TableHeaderCell>Title</TableHeaderCell>
            <TableHeaderCell>Date</TableHeaderCell>
            <TableHeaderCell>Created at</TableHeaderCell>
            <TableHeaderCell>Place</TableHeaderCell>
            <TableHeaderCell>Moderator</TableHeaderCell>
            <TableHeaderCell>Edit</TableHeaderCell>
            <TableHeaderCell>Delete</TableHeaderCell>
          </TableRow>
        </TableHeader>

        <TableBody>
          {getArray.map((event) => (
            <>
              <EventForSellersList event={event} />
            </>
          ))}
        </TableBody>

        <TableFooter fullWidth>
          <TableRow>
            <TableHeaderCell colSpan="7">
              <Button
                floated="right"
                icon="add"
                labelPosition="left"
                primary
                size="small"
                // onClick={() => modalStore.openModal(<EventSellersForm closeModal={modalStore.closeModal} />)}
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
  );
}

export default observer(OwnedEventList);
