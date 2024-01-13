import { observer } from 'mobx-react';
import { Outlet } from 'react-router';
import { ToastContainer } from 'react-toastify';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';


function App() {


    return (
        <div>
            <ToastContainer position='bottom-right' hideProgressBar theme='colored' />
            {/* <ScrollRestoration /> */}
            {/* <ModalContainer /> */}
            {
                // location.pathname === '/' ? <HomePage />
                //     : 
                (
                    <>
                        <NavBar />
                        <Container fluid>
                            <Outlet />
                        </Container>
                    </>
                )
            }
        </div>
    );

}

export default observer(App);