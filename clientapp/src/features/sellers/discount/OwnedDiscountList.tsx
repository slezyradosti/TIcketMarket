import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { useEffect } from "react";
import { Button, Icon, Table, TableBody, TableFooter, TableHeader, TableHeaderCell, TableRow } from "semantic-ui-react";
import TicketDiscountForList from "./TicketDiscountForList";
import { Link } from "react-router-dom";


function OwnedDiscountList() {
    const { ticketDiscountStore } = useStore();
    const { getArray } = ticketDiscountStore;

    useEffect(() => {
        ticketDiscountStore.loadSellersList();
    }, [ticketDiscountStore]);

    return (
        <>
            <Table compact celled>
                <TableHeader>
                    <TableRow>
                        <TableHeaderCell>Percentage</TableHeaderCell>
                        <TableHeaderCell>Created at</TableHeaderCell>
                        <TableHeaderCell>Activated</TableHeaderCell>
                        <TableHeaderCell>Edit</TableHeaderCell>
                        <TableHeaderCell>Delete</TableHeaderCell>
                    </TableRow>
                </TableHeader>

                <TableBody>
                    {getArray.map((discount) => (
                        <>
                            <TicketDiscountForList ticketDiscount={discount} />
                        </>
                    ))}
                </TableBody>

                <TableFooter fullWidth>
                    <TableRow>
                        <TableHeaderCell colSpan='5'>
                            <Button
                                floated='right'
                                icon
                                labelPosition='left'
                                primary
                                size='small'
                                as={Link} to='manage'
                            >
                                <Icon name='add' /> Add new
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

export default observer(OwnedDiscountList);
