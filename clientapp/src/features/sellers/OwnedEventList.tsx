import { observer } from "mobx-react";
import { useStore } from "../../app/stores/store";
import { Grid } from "semantic-ui-react";
import { useEffect } from "react";
import EventForList from "../EventForList";

function OwnedEventList() {
    const { eventStore } = useStore();
    const { getArray } = eventStore;

    useEffect(() => {
        eventStore.loadSellersList();
    }, [eventStore]);

    return (
        <>
            <Grid columns={2} divided>

                {getArray.map((event) => (
                    <>
                        <EventForList event={event} />
                    </>
                ))}
            </Grid>
        </>
    );
}

export default observer(OwnedEventList);
