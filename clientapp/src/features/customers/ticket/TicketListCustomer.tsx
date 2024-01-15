import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Grid } from "semantic-ui-react";
import { useEffect } from "react";
import TicketForList from "./TicketForList";

function TicketListCustomer() {
    const { ticketStore } = useStore();
    const { getArray } = ticketStore;

    useEffect(() => {
        ticketStore.loadCustomersList();
    }, [ticketStore]);

    return (
        <>
            <Grid columns={2} divided>

                {getArray.map((ticket) => (
                    <>
                        <TicketForList ticket={ticket} />
                    </>
                ))}
            </Grid>
        </>
    );
}

export default observer(TicketListCustomer);
