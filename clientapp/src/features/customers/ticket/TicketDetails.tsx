import { observer } from "mobx-react";
import { Item, ItemContent, ItemDescription, ItemExtra, ItemGroup, ItemHeader, ItemImage, ItemMeta, Image } from "semantic-ui-react";
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import { useStore } from "../../../app/stores/store";

function EventDetails() {
    const { ticketStore } = useStore();
    const { detailsElement } = ticketStore;
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            ticketStore.detailsCustomers(id);
        }
    }, [ticketStore, id])

    return (
        <>
            <ItemGroup>
                <Item>
                    <ItemImage size='large' src='/images/wireframe/image.png' />

                    <ItemContent>
                        <ItemHeader as='a'>Number: {detailsElement?.number}</ItemHeader>
                        <ItemMeta>Price: {detailsElement?.finalPrice}</ItemMeta>
                        <ItemMeta>Type: {detailsElement?.type?.type}</ItemMeta>
                        <ItemDescription>
                            <Image src='/images/wireframe/short-paragraph.png' />
                        </ItemDescription>
                        <ItemExtra>Discount: {detailsElement?.discount?.discountPercentage}</ItemExtra>
                    </ItemContent>
                </Item>
            </ItemGroup>
        </>
    );
}

export default observer(EventDetails);
