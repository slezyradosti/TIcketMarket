import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { Grid, Header, Image } from "semantic-ui-react";
import { useEffect } from "react";
import OrderForList from "./OrderForList";

function OrderListCustomer() {
    const { profileStore } = useStore();
    const { getOrderArray, loadingOrdersInitial } = profileStore;

    useEffect(() => {
        profileStore.loadCustomersOrderList();
    }, [profileStore]);

    return (
        <>
            <Header as='h3'>My Orders</Header>
            {
                loadingOrdersInitial
                    ? <><Image src='https://react.semantic-ui.com/images/wireframe/paragraph.png' /></>
                    : (<>
                        <Grid columns={2} divided>

                            {getOrderArray.map((order) => (
                                <>
                                    <OrderForList order={order} />
                                </>
                            ))}
                        </Grid>
                    </>)}
        </>
    );
}

export default observer(OrderListCustomer);
