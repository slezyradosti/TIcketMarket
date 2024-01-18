import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Grid } from "semantic-ui-react";
import { useEffect } from "react";
import OrderForList from "./OrderForList";

function OrderListCustomer() {
    const { profileStore } = useStore();
    const { getOrderArray } = profileStore;

    useEffect(() => {
        profileStore.loadCustomersOrderList();
    }, [profileStore]);

    return (
        <>
            <Grid columns={2} divided>

                {getOrderArray.map((order) => (
                    <>
                        <OrderForList order={order} />
                    </>
                ))}
            </Grid>
        </>
    );
}

export default observer(OrderListCustomer);
