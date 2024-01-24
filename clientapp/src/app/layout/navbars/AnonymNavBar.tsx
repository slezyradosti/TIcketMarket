import { observer } from "mobx-react";
import { useState } from "react";
import { Dropdown, DropdownItem, DropdownMenu, Menu, MenuItem, MenuMenu } from "semantic-ui-react";
import { useStore } from "../../stores/store";
import LoginForm from "../../../features/user/LoginForm";
import RegisterForm from "../../../features/user/RegisterForm";
import { Link } from "react-router-dom";

function AnonymNavBar() {
    const [activeItem, setActiveItem] = useState<string>();
    const handleItemClick = (name: string) => setActiveItem(name);
    const { modalStore } = useStore();


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
                    as={Link} to='home'
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
                                text='Log in'
                                onClick={() => modalStore.openModal(<LoginForm />)}
                            />
                            <DropdownItem
                                text='Register'
                                onClick={() => modalStore.openModal(<RegisterForm />)}
                            />
                        </DropdownMenu>
                    </Dropdown>
                </MenuMenu>
            </Menu >
        </>
    );
}

export default observer(AnonymNavBar);