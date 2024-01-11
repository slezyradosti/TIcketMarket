import { observer } from "mobx-react";
import { useState } from "react";
import { Dropdown, Header, Menu, MenuItem } from "semantic-ui-react";

function CustomerNavBar() {
    const [activeItem, setActiveItem] = useState<string>();

    const handleItemClick = (name: string) => setActiveItem(name);

    const dropdownOptions = [
        { key: 'my_orders', text: 'My Orders', value: 'my_orders' },
        { key: 'my_tickets', text: 'My tickets', value: 'my_tikets' },
        { key: 'logout', text: 'Logout', value: 'logout' },
    ]

    return (
        <>
            <Header>
                Ticket Market
            </Header>
            <Menu>
                <MenuItem
                    name='events'
                    active={activeItem === 'events'}
                    onClick={() => handleItemClick('events')}
                >
                    Events
                </MenuItem>
            </Menu>

            <Dropdown
                button
                className='icon'
                floating
                labeled
                icon='user outline'
                options={dropdownOptions}
                text='Select Language'
            />
        </>
    );
}

export default observer(CustomerNavBar);