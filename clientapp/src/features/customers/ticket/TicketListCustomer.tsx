import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Grid, Header, Image } from "semantic-ui-react";
import { useEffect } from "react";
import TicketForList from "./TicketForList";

function TicketListCustomer() {
    const { profileStore } = useStore();
    const { getTicketArray, loadingTicketsInitial } = profileStore;

    useEffect(() => {
        profileStore.loadCustomersTicketList();
    }, [profileStore]);

    return (
        <>
            <Header as='h3'>My Tickets</Header>
            {
                loadingTicketsInitial
                    ? <><Image src='https://react.semantic-ui.com/images/wireframe/paragraph.png' /></>
                    : (<>
                        <Grid columns={2} divided>

                            {getTicketArray.map((ticket) => (
                                <>
                                    <TicketForList ticket={ticket} />
                                </>
                            ))}
                        </Grid>
                    </>)}
        </>
    );
}

export default observer(TicketListCustomer);
