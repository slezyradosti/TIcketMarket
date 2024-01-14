import {
    createBrowserRouter,
    Navigate,
    RouteObject,
} from "react-router-dom";
import App from "../layout/App";
import RequireAuth from "./requireAuth";
import HomeCustomer from "../../features/customers/HomeCustomer";
import OwnedEventList from "../../features/sellers/OwnedEventList";


export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomeCustomer /> },
            // { path: 'not-found', element: <NotFound /> },
            // { path: 'server-error', element: <ServerError /> },
            // { path: '*', element: <Navigate replace to='/not-found' /> },
            { path: 'event/my-events', element: < OwnedEventList /> },
            { path: 'event/list', element: < HomeCustomer /> },
            // {
            //     element: <RequireAuth />,
            //     children: [

            //         { path: 'notebooks', element: <Dashboard /> },
            //         { path: 'notebooks/:id', element: <Dashboard /> },
            //         { path: 'units', element: <Dashboard /> },
            //         { path: 'units/:id', element: <Dashboard /> },
            //         { path: 'pages', element: <Dashboard /> },
            //         { path: 'pages/:id', element: <Dashboard /> },
            //         { path: 'notes', element: <Dashboard /> },
            //         { path: 'notes/:id', element: <Dashboard /> },
            //         { path: 'profiles/:id', element: <ProfilePage /> }
            //     ],
            // },

        ]
    },
]

export const router = createBrowserRouter(routes)