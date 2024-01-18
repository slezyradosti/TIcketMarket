import {
    createBrowserRouter,
    Navigate,
    RouteObject,
} from "react-router-dom";
import App from "../layout/App";
import RequireAuth from "./requireAuth";
import HomeCustomer from "../../features/customers/HomeCustomer";
import OwnedEventList from "../../features/sellers/OwnedEventList";
import OwnedDiscountList from "../../features/sellers/discount/OwnedDiscountList";
import OrderListCustomer from "../../features/customers/order/OrderListCustomer";
import TicketListCustomer from "../../features/customers/ticket/TicketListCustomer";
import CustomerProfile from "../../features/profile/CustomerProfile";


export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomeCustomer /> },
            { path: 'home', element: <HomeCustomer /> },
            // { path: 'not-found', element: <NotFound /> },
            // { path: 'server-error', element: <ServerError /> },
            // { path: '*', element: <Navigate replace to='/not-found' /> },

            { path: 'event/list', element: < HomeCustomer /> },
            {
                element: <RequireAuth />,
                children: [

                    { path: 'event/my-events', element: < OwnedEventList /> },
                    { path: 'TicketDiscount/my-discounts', element: < OwnedDiscountList /> },
                    { path: 'Order/my-orders', element: < OrderListCustomer /> },
                    { path: 'Ticket/my-tickets', element: < TicketListCustomer /> },
                    {
                        path: 'profile',
                        element: < CustomerProfile />,
                        children: [
                            { path: 'profile/my-orders', element: < OrderListCustomer /> },
                            { path: 'profile/my-tickets', element: < TicketListCustomer /> },
                        ]
                    },
                ],
            },

        ]
    },
]

export const router = createBrowserRouter(routes)