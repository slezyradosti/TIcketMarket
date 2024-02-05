import { observer } from "mobx-react";
import { useState } from "react";
import { Link } from "react-router-dom";
import { Dropdown, DropdownItem, DropdownMenu, Menu, MenuItem, MenuMenu } from "semantic-ui-react";
import { useStore } from "../../stores/store";

function SellerNavBar() {
    const { userStore } = useStore();
    const [activeItem, setActiveItem] = useState<string>();

    const handleItemClick = (name: string) => setActiveItem(name);

    const handleLogout = () => userStore.logout();

    return (
        <>
            <Menu color='blue' inverted style={{ borderRadius: '0' }}>
                <MenuItem
                    as={Link} to='event/my-events'
                    name='My Events'
                    active={activeItem === 'my_events'}
                    onClick={() => handleItemClick('my_events')}
                />
                <MenuItem
                    as={Link} to='TicketDiscount/my-discounts'
                    name='My Discounts'
                    active={activeItem === 'my_discounts'}
                    onClick={() => handleItemClick('my_discounts')}
                />
                <MenuItem
                    as={Link} to='TableEvent/my-list'
                    name='My Table-Events'
                    active={activeItem === 'my_list'}
                    onClick={() => handleItemClick('my_list')}
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

export default observer(SellerNavBar);