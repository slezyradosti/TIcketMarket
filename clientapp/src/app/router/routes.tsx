import {
    createBrowserRouter,
    Navigate,
    RouteObject,
} from "react-router-dom";
import App from "../layout/App";
import RequireAuth from "./requireAuth";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            { path: 'login', element: <HomePage /> },
            { path: 'register', element: <HomePage /> },
            { path: 'not-found', element: <NotFound /> },
            { path: 'server-error', element: <ServerError /> },
            { path: '*', element: <Navigate replace to='/not-found' /> },
            { path: 'ticket/buy-ticket', element: <ServerError /> },
            {
                element: <RequireAuth />,
                children: [

                    { path: 'notebooks', element: <Dashboard /> },
                    { path: 'notebooks/:id', element: <Dashboard /> },
                    { path: 'units', element: <Dashboard /> },
                    { path: 'units/:id', element: <Dashboard /> },
                    { path: 'pages', element: <Dashboard /> },
                    { path: 'pages/:id', element: <Dashboard /> },
                    { path: 'notes', element: <Dashboard /> },
                    { path: 'notes/:id', element: <Dashboard /> },
                    { path: 'profiles/:id', element: <ProfilePage /> }
                ],
            },

        ]
    },
]

export const router = createBrowserRouter(routes)