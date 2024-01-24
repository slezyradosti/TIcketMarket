import { observer } from "mobx-react";
import { Item, ItemContent, ItemDescription, ItemExtra, ItemGroup, ItemHeader, ItemImage, ItemMeta, Image } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { useParams } from "react-router-dom";
import { useEffect } from "react";

function EventDetails() {
    const { eventStore } = useStore();
    const { detailsElement } = eventStore;
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            eventStore.details(id);
        }
    }, [eventStore, id])

    return (
        <>
            <ItemGroup>
                <Item>
                    <ItemImage size='large' src='/images/wireframe/image.png' />

                    <ItemContent>
                        <ItemHeader as='a'>{detailsElement?.title}</ItemHeader>
                        <ItemMeta>{detailsElement?.description}</ItemMeta>
                        <ItemDescription>
                            <Image src='/images/wireframe/short-paragraph.png' />
                        </ItemDescription>
                        <ItemExtra>{detailsElement?.date.toLocaleDateString()}</ItemExtra>
                    </ItemContent>
                </Item>
            </ItemGroup>
        </>
    );
}

export default observer(EventDetails);
