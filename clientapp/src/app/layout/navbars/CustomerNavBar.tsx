import { observer } from "mobx-react";
import { useState } from "react";
import { Dropdown, DropdownItem, DropdownMenu, Menu, MenuItem, MenuMenu } from "semantic-ui-react";

function CustomerNavBar() {
    const [activeItem, setActiveItem] = useState<string>();

    const handleItemClick = (name: string) => setActiveItem(name);
    return (
        <>
            <Menu color='blue' inverted style={{ borderRadius: '0' }}>
                <MenuItem
                    name='Home'
                    active={activeItem === 'home'}
                    onClick={() => handleItemClick('home')}
                />
                <MenuItem
                    name='Events'
                    active={activeItem === 'events'}
                    onClick={() => handleItemClick('events')}
                />

                <MenuMenu position='right'>
                    <Dropdown
                        item
                        text='Account'
                        icon='user outline'
                        floating
                        labeled
                        button
                        className='icon'
                    >
                        <DropdownMenu>
                            <DropdownItem
                                text='My orders'
                            />
                            <DropdownItem
                                text='My Tickets'
                            />
                            <DropdownItem
                                text='Log out'
                            />
                        </DropdownMenu>
                    </Dropdown>
                </MenuMenu>
            </Menu >
        </>
    );
}

export default observer(CustomerNavBar);