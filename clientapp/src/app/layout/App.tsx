import { observer } from 'mobx-react';
import { Outlet } from 'react-router';
import { ToastContainer } from 'react-toastify';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import ModalContainer from '../common/ModalContainer';
import { useStore } from '../stores/store';
import { useEffect } from 'react';


function App() {
    const { commonStore, userStore } = useStore();

    // if pages reloading but userToken is sill alive
    useEffect(() => {
        if (commonStore.token) {
            userStore.getUser().finally(() => commonStore.setAppLoaded())
        } else {
            commonStore.setAppLoaded()
        }
    }, [commonStore, userStore])

    return (
        <div>
            <ToastContainer position='bottom-right' hideProgressBar theme='colored' />
            {/* <ScrollRestoration /> */}
            <ModalContainer />
            {
                (
                    <>
                        <NavBar />
                        <Container>
                            <Outlet />
                        </Container>
                    </>
                )
            }
        </div>
    );

}

export default observer(App);