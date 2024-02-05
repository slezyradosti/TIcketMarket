import { observer } from "mobx-react";
import { TableRow, TableCell, Button, Icon } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { Link } from "react-router-dom";
import { TableEvent } from "../../../app/models/tables/tableEvent";

interface Props {
    tableEvent: TableEvent;
}

function TableEventForList({ tableEvent }: Props) {
    const { tableEventStore } = useStore();
    const { loading, deleteOne } = tableEventStore

    const handleDelete = (tEventId: string) => {
        deleteOne(tEventId);
    }

    return (
        <>
            <TableRow>
                <TableCell> <a href={`/tableevent/my-list/${tableEvent?.id}`}>{tableEvent?.table?.number}</a> </TableCell>
                <TableCell>{tableEvent?.table?.price}</TableCell>
                <TableCell>{tableEvent?.table?.peopleQuantity}</TableCell>
                <TableCell>{tableEvent?.table?.createdAt?.toLocaleDateString()}</TableCell>
                <TableCell>
                    <Button
                        as={Link} to={`manage/${tableEvent?.id}`}
                    >
                        Edit
                    </Button>
                </TableCell>
                <TableCell>
                    <Button
                        icon
                        onClick={() => handleDelete(tableEvent!.id)}
                        loading={loading}
                    >
                        <Icon name="trash alternate outline" />
                    </Button>
                </TableCell>
            </TableRow>
        </>
    );
}

export default observer(TableEventForList);
