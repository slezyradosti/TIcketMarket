import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Button, Dropdown, Icon, Table, TableBody, TableFooter, TableHeader, TableHeaderCell, TableRow } from "semantic-ui-react";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import TableEventForList from "./TableEventForList";

function OwnedTableEventList() {
    const { eventSellersStore, tableEventStore } = useStore();
    const [chosenEvent, setChosenEvent] = useState<string | null>(null);

    useEffect(() => {
        console.log('useeffect:' + chosenEvent)
        if (chosenEvent == null) {
            tableEventStore.clearData();
            eventSellersStore.loadOptions();
        }
        else if (chosenEvent) {
            tableEventStore.loadList(chosenEvent);
        }
    }, [tableEventStore, chosenEvent]);

    const handleEventSelectChange = (event, data) => {
        setChosenEvent(data.value);
    }

    return (
        <>
            <div className="ui thingy">
                <Dropdown
                    placeholder='Event'
                    search
                    selection
                    options={eventSellersStore.eventOptions}
                    onChange={(e, data) => handleEventSelectChange(e, data)}
                />
            </div >


            <Table compact celled>
                <TableHeader>
                    <TableRow>
                        <TableHeaderCell>Table number</TableHeaderCell>
                        <TableHeaderCell>Table price</TableHeaderCell>
                        <TableHeaderCell>Table People Quantity</TableHeaderCell>
                        <TableHeaderCell>Created at</TableHeaderCell>
                        <TableHeaderCell>Edit</TableHeaderCell>
                        <TableHeaderCell>Delete</TableHeaderCell>
                    </TableRow>
                </TableHeader>

                <TableBody>
                    {tableEventStore.getArray.map((tableEvent) => (
                        <>
                            <TableEventForList tableEvent={tableEvent} />
                        </>
                    ))}
                </TableBody>

                <TableFooter fullWidth>
                    <TableRow>
                        <TableHeaderCell colSpan='6'>
                            <Button
                                floated='right'
                                icon='add'
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

export default observer(OwnedTableEventList);
