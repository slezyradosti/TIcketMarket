import { observer } from "mobx-react";
import { Item, ItemContent, ItemDescription, ItemExtra, ItemGroup, ItemHeader, ItemImage, ItemMeta, Image } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { useParams } from "react-router-dom";
import { useEffect } from "react";

function EventDetails() {
    const { eventCustomersStore } = useStore();
    const { detailsElement } = eventCustomersStore;
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            eventCustomersStore.details(id);
        }
    }, [eventCustomersStore, id])

    return (
        <>
            <ItemGroup>
                <Item>
                    <ItemImage size='large' src='/images/wireframe/image.png' />

                    <ItemContent>
                        <ItemHeader as='a'>{detailsElement?.title}</ItemHeader>
                        <ItemMeta>Discription: {detailsElement?.description}</ItemMeta>
                        <ItemDescription>
                            <Image src='/images/wireframe/short-paragraph.png' />
                        </ItemDescription>
                        <ItemExtra>Date: {detailsElement?.date?.toLocaleDateString()}</ItemExtra>
                    </ItemContent>
                </Item>
            </ItemGroup>
        </>
    );
}

export default observer(EventDetails);
