import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Grid } from "semantic-ui-react";
import { useEffect } from "react";
import OrderForList from "./OrderForList";

function OrderListCustomer() {
    const { orderStore } = useStore();
    const { getArray } = orderStore;

    useEffect(() => {
        orderStore.loadCustomersList();
    }, [orderStore]);

    return (
        <>
            <Grid columns={2} divided>

                {getArray.map((order) => (
                    <>
                        <OrderForList order={order} />
                    </>
                ))}
            </Grid>
        </>
    );
}

export default observer(OrderListCustomer);
