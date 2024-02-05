import {
    createBrowserRouter,
    Navigate,
    RouteObject,
} from "react-router-dom";
import App from "../layout/App";
import RequireAuth from "./requireAuth";
import HomeCustomer from "../../features/customers/HomeCustomer";
import OwnedEventList from "../../features/sellers/event/OwnedEventList";
import OwnedDiscountList from "../../features/sellers/discount/OwnedDiscountList";
import OrderListCustomer from "../../features/customers/order/OrderListCustomer";
import TicketListCustomer from "../../features/customers/ticket/TicketListCustomer";
import CustomerProfile from "../../features/profile/CustomerProfile";
import UserDetails from "../../features/profile/UserDetails";
import EventDetails from "../../features/event/EventDetails";
import EventSellersDetails from "../../features/sellers/event/EventSellersDetails";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import EventSellersForm from "../../features/sellers/forms/EventSellersForm";
import TicketDiscountSellersForm from "../../features/sellers/forms/TicketDiscountSellersForm";


export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomeCustomer /> },
            { path: 'home', element: <HomeCustomer /> },
            { path: 'event/:id', element: <EventDetails /> },
            { path: 'not-found', element: <NotFound /> },
            { path: 'server-error', element: <ServerError /> },
            { path: '*', element: <Navigate replace to='/not-found' /> },

            { path: 'event/list', element: < HomeCustomer /> },
            {
                element: <RequireAuth />,
                children: [

                    { path: 'event/my-events', element: < OwnedEventList /> },
                    { path: 'event/my-events/:id', element: < EventSellersDetails /> },
                    { path: 'event/my-events/manage/:id?', element: < EventSellersForm /> },
                    { path: 'TicketDiscount/my-discounts', element: < OwnedDiscountList /> },
                    { path: 'TicketDiscount/my-discounts/manage/:id?', element: < TicketDiscountSellersForm /> },
                    { path: 'Order/my-orders', element: < OrderListCustomer /> },
                    { path: 'Ticket/my-tickets', element: < TicketListCustomer /> },
                    {
                        path: 'profile',
                        element: < CustomerProfile />,
                        children: [
                            { path: 'my-orders', element: < OrderListCustomer /> },
                            { path: 'my-tickets', element: < TicketListCustomer /> },
                            { path: 'details', element: < UserDetails /> },
                        ]
                    },
                ],
            },

        ]
    },
]

export const router = createBrowserRouter(routes)