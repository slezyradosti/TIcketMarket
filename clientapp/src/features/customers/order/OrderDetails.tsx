// import { observer } from "mobx-react";
// import { Item, ItemContent, ItemDescription, ItemExtra, Image, ItemHeader, ItemImage, ItemMeta } from "semantic-ui-react";
// import { Order } from "../../../app/models/tables/order";

// interface Props {
//     order: Order;
// }

// function OrderDetails({ order }: Props) {
//     return (
//         <>

//             <Item>
//                 <ItemImage size='tiny' src='https://react.semantic-ui.com/images/wireframe/image.png' />

//                 <ItemContent>
//                     <ItemHeader as='a'>{order.id}</ItemHeader>
//                     <ItemMeta>Created on: {order.createdAt.toLocaleDateString()}</ItemMeta>
//                     <ItemDescription>
//                         <Image src='https://react.semantic-ui.com/images/wireframe/short-paragraph.png' />
//                     </ItemDescription>
//                     <ItemExtra>Additional Details</ItemExtra>
//                 </ItemContent>
//             </Item>
//         </>
//     );
// }

// export default observer(OrderDetails);
