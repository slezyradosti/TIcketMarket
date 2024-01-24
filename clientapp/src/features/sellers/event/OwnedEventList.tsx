import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Grid } from "semantic-ui-react";
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
            <Grid columns={2} divided>

                {getArray.map((event) => (
                    <>
                        <EventForSellersList event={event} />
                    </>
                ))}
            </Grid>
        </>
    );
}

export default observer(OwnedEventList);
