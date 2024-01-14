import {
    createBrowserRouter,
    Navigate,
    RouteObject,
} from "react-router-dom";
import App from "../layout/App";
import RequireAuth from "./requireAuth";
import HomeCustomer from "../../features/customers/HomeCustomer";
import OwnedEventList from "../../features/sellers/OwnedEventList";
import OwnedDiscountList from "../../features/sellers/OwnedDiscountList";


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
                ],
            },

        ]
    },
]

export const router = createBrowserRouter(routes)