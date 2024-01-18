import { observer } from "mobx-react";
import { Header, Icon, Menu, MenuItem, Segment, Sidebar, SidebarPushable, SidebarPusher, Image } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { Outlet } from "react-router-dom";
import { Link } from "react-router-dom";

function CustomerProfile() {
    const { userStore } = useStore();
    const { user } = userStore;


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
                <MenuItem as='a'>
                    <Icon name='user outline' />
                    {user?.firstname} {user?.lastname}
                </MenuItem>
                <MenuItem as={Link} to='profile/my-orders'>
                    <Icon name='file alternate outline' />
                    My Orders
                </MenuItem>
                <MenuItem as={Link} to='profile/my-tickets'>
                    <Icon name='ticket' />
                    My Tickets
                </MenuItem>
            </Sidebar>

            <SidebarPusher>
                <Segment basic>
                    <Header as='h3'>Application Content</Header>
                    {/* <Image src='https://react.semantic-ui.com/images/wireframe/paragraph.png' /> */}

                    <Outlet />
                </Segment>
            </SidebarPusher>
        </SidebarPushable>
    );
}

export default observer(CustomerProfile);