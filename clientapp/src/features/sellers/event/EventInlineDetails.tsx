import { observer } from "mobx-react";
import { useEffect } from "react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Item, ItemContent, ItemDescription, ItemHeader } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";

interface Props {
    eventId: string;
}

function EventInlineDetails({ eventId }: Props) {
    const { eventSellersStore } = useStore();
    const { detailsSellers, detailsElement } = eventSellersStore;

    useEffect(() => {
        detailsSellers(eventId);
    }, [eventSellersStore, eventId])

    if (eventSellersStore.loading) {
        return <LoadingComponent content='Loading app...' />
    }

    return (
        <>
            <Item>
                <ItemContent>
                    <ItemHeader>Title: {detailsElement?.title}</ItemHeader>
                    <ItemDescription>Date: {detailsElement?.date.toLocaleDateString()}</ItemDescription>
                    <ItemDescription>Place: {detailsElement?.place}</ItemDescription>
                    <ItemDescription>Total Places: {detailsElement?.totalPlaces}</ItemDescription>
                    <ItemDescription>Type: {detailsElement?.type?.type}</ItemDescription>
                    <ItemDescription>Category: {detailsElement?.category?.category}</ItemDescription>
                </ItemContent>
            </Item>
        </>
    );
}

export default observer(EventInlineDetails);