import { observer } from "mobx-react";
import { useState } from "react";
import { Dropdown, DropdownItem, DropdownMenu, Menu, MenuItem, MenuMenu } from "semantic-ui-react";
import { useStore } from "../../stores/store";
import { Link } from "react-router-dom";

function CustomerNavBar() {
    const { userStore } = useStore();
    const [activeItem, setActiveItem] = useState<string>();

    const handleItemClick = (name: string) => setActiveItem(name);

    const handleLogout = () => userStore.logout();

    return (
        <>
            <Menu color='blue' inverted style={{ borderRadius: '0' }}>
                <MenuItem
                    as={Link} to='home'
                    name='Home'
                    active={activeItem === 'home'}
                    onClick={() => handleItemClick('home')}
                />
                <MenuItem
                    as={Link} to='event/list'
                    name='Events'
                    active={activeItem === 'events'}
                    onClick={() => handleItemClick('events')}
                />

                <MenuMenu position='right'>
                    <Dropdown
                        item
                        text={userStore.user?.firstname}
                        icon='user outline'
                        floating
                        labeled
                        button
                        className='icon'
                    >
                        <DropdownMenu>
                            <DropdownItem
                                as={Link} to='profile'
                                text='Profile'
                            />
                            <DropdownItem
                                text='Logout'
                                onClick={() => handleLogout()}
                            />
                        </DropdownMenu>
                    </Dropdown>
                </MenuMenu>
            </Menu >
        </>
    );
}

export default observer(CustomerNavBar);