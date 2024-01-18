import { observer } from "mobx-react";
import { Icon, Menu, MenuItem, Segment, Sidebar, SidebarPushable, SidebarPusher, Image } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { Outlet } from "react-router-dom";
import { Link } from "react-router-dom";

function CustomerProfile() {
    const { userStore, profileStore } = useStore();
    const { user } = userStore;
    const { loadingInitial, ticketRegistry, orderRegistry } = profileStore;


    return (
        <SidebarPushable as={Segment}>
            <Sidebar
                as={Menu}
                animation='slide along'
                direction='left'
                icon='labeled'
                vertical
                visible
                width='thin'
            >
                <MenuItem as={Link} to='details'>
                    <Icon name='user outline' />
                    {user?.firstname} {user?.lastname}
                </MenuItem>
                <MenuItem as={Link} to='my-orders'>
                    <Icon name='file alternate outline' />
                    My Orders
                </MenuItem>
                <MenuItem as={Link} to='my-tickets'>
                    <Icon name='ticket' />
                    My Tickets
                </MenuItem>
            </Sidebar>

            <SidebarPusher>
                <Segment basic>
                    <Outlet />
                </Segment>
            </SidebarPusher>
        </SidebarPushable>
    );
}

// eslint-disable-next-line react-refresh/only-export-components
export default observer(CustomerProfile);