import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Button, Grid, Icon, Table, TableBody, TableFooter, TableHeader, TableHeaderCell, TableRow } from "semantic-ui-react";
import { useEffect } from "react";
import EventForSellersList from "./EventForSellersList";

function OwnedEventList() {
    const { eventSellersStore } = useStore();
    const { getArray } = eventSellersStore;

    useEffect(() => {
        eventSellersStore.loadList();
    }, [eventSellersStore]);

    return (
        <>
            <Table compact celled>
                <TableHeader>
                    <TableRow>
                        <TableHeaderCell>Title</TableHeaderCell>
                        <TableHeaderCell>Date</TableHeaderCell>
                        <TableHeaderCell>Created at</TableHeaderCell>
                        <TableHeaderCell>Place</TableHeaderCell>
                        <TableHeaderCell>Type</TableHeaderCell>
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
                        <TableHeaderCell colSpan='7'>
                            <Button
                                floated='right'
                                icon
                                labelPosition='left'
                                primary
                                size='small'
                            >
                                <Icon name='user' /> Add User
                            </Button>
                            <Button size='small'>Approve</Button>
                            <Button disabled size='small'>
                                Approve All
                            </Button>
                        </TableHeaderCell>
                    </TableRow>
                </TableFooter>
            </Table>
        </>
    );
}

export default observer(OwnedEventList);
