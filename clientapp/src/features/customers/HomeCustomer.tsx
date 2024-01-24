import { observer } from "mobx-react";
import { useStore } from "../../app/stores/store";
import { Grid } from "semantic-ui-react";
import { useEffect } from "react";
import EventForCustomersList from "../event/EventForCustomersList";

function HomeCustomer() {
    const { eventCustomersStore } = useStore();
    const { getArray } = eventCustomersStore;

    useEffect(() => {
        eventCustomersStore.loadList();
    }, [eventCustomersStore]);

    return (
        <>
            <Grid columns={2} divided>

                {getArray.map((event) => (
                    <>
                        <EventForCustomersList event={event} />
                    </>
                ))}
            </Grid>
        </>
    );
}

export default observer(HomeCustomer);
