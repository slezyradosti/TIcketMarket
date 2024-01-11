import { observer } from "mobx-react";
import { useState } from "react";
import { Dropdown, Header, Menu, MenuItem } from "semantic-ui-react";

function SellerNavBar() {
    const [activeItem, setActiveItem] = useState<string>();

    const handleItemClick = (name: string) => setActiveItem(name);

    const dropdownOptions = [
        { key: 'logout', text: 'Logout', value: 'logout' },
    ]

    return (
        <>
            <Header>
                Ticket Market
            </Header>
            <Menu>
                <MenuItem
                    name='my_events'
                    active={activeItem === 'my_events'}
                    onClick={() => handleItemClick('my_events')}
                >
                    My Events
                </MenuItem>

                <MenuItem
                    name='my_discounts'
                    active={activeItem === 'my_discounts'}
                    onClick={() => handleItemClick('my_discounts')}
                >
                    My Discounts
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

export default observer(SellerNavBar);