import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Grid } from "semantic-ui-react";
import { useEffect } from "react";
import TicketForList from "./TicketForList";

function TicketListCustomer() {
    const { profileStore } = useStore();
    const { getTicketArray } = profileStore;

    useEffect(() => {
        profileStore.loadCustomersTicketList();
    }, [profileStore]);

    return (
        <>
            <Grid columns={2} divided>

                {getTicketArray.map((ticket) => (
                    <>
                        <TicketForList ticket={ticket} />
                    </>
                ))}
            </Grid>
        </>
    );
}

export default observer(TicketListCustomer);
