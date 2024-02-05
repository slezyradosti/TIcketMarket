import { observer } from "mobx-react";
import { Item, ItemContent, ItemHeader, ItemDescription } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { useEffect } from "react";
import LoadingComponent from "../../../app/layout/LoadingComponent";

interface Props {
    tableId: string;
}

function EventTableSellersDetails({ tableId }: Props) {
    const { eventTableStore } = useStore();
    const { details, detailsElement } = eventTableStore;

    useEffect(() => {
        details(tableId);
    }, [eventTableStore, tableId])

    if (eventTableStore.loading) {
        return <LoadingComponent content='Loading app...' />
    }

    return (
        <>
            <Item>
                <ItemContent>
                    <ItemHeader>Price: {detailsElement?.price}</ItemHeader>
                    <ItemDescription>People Quantity: {detailsElement?.peopleQuantity}</ItemDescription>
                    <ItemDescription>People Quantity: {detailsElement?.createdAt?.toLocaleDateString()}</ItemDescription>
                </ItemContent>
            </Item>
        </>
    );
}

export default observer(EventTableSellersDetails);
