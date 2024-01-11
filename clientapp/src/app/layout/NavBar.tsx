import { useNavigate } from "react-router-dom";
import { useStore } from "../stores/store";
import { Roles } from "../models/roles";
import { useEffect } from "react";
import { observer } from "mobx-react";
import CustomerNavBar from "./navbars/CustomerNavBar";
import SellerNavBar from "./navbars/SellerNavBar";

function NavBar() {
    const { userStore: { user, logout, userRights, getUserRights, } } = useStore();
    const navigate = useNavigate();

    useEffect(() => {
        getUserRights();
    }, [user])

    return (
        <>
            {userRights == Roles.Seller
                ? <> <SellerNavBar /> </>
                : <> {
                    userRights == Roles.Customer
                        ? <> <CustomerNavBar /> </>
                        : <></>
                }
                </>
            }
        </>
    );
}

export default observer(NavBar);