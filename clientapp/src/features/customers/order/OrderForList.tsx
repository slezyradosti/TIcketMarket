import { observer } from "mobx-react";
import { GridColumn, GridRow, ItemDescription, ItemHeader } from "semantic-ui-react";
import { Order } from "../../../app/models/tables/order";

interface Props {
    order: Order;
}

function OrderForList({ order }: Props) {
    return (
        <>
            <GridRow key={order.id}>
                <GridColumn width={12}>
                    <ItemHeader>Name:  {order.user?.firstname}  {order.user?.lastname}</ItemHeader>
                    <ItemDescription>Created at: {order.createdAt.toLocaleDateString()}</ItemDescription>
                </GridColumn>
            </GridRow>
        </>
    );
}

export default observer(OrderForList);
