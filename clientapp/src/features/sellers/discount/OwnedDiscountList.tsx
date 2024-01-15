import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Grid } from "semantic-ui-react";
import { useEffect } from "react";
import TicketDiscountForList from "./TicketDiscountForList";

function OwnedDiscountList() {
    const { ticketDiscountStore } = useStore();
    const { getArray } = ticketDiscountStore;

    useEffect(() => {
        ticketDiscountStore.loadSellersList();
    }, [ticketDiscountStore]);

    return (
        <>
            <Grid columns={2} divided>

                {getArray.map((discount) => (
                    <>
                        <TicketDiscountForList ticketDiscount={discount} />
                    </>
                ))}
            </Grid>
        </>
    );
}

export default observer(OwnedDiscountList);
