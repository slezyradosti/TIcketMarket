import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useStore } from "../stores/store";

function RequireAuth() {
    const { userStore: { isLoggedIn } } = useStore();
    const location = useLocation();

    if (!isLoggedIn) {
        return <Navigate to='/home' state={location} />
    }

    return <Outlet />
}

export default RequireAuth;