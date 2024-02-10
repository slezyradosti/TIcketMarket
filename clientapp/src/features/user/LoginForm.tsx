import { Button, Container, Header, Input, Label } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react";
import { ErrorMessage, Field, Form, Formik } from "formik";
import * as Yup from 'yup';
import TextInputFormik from "../../app/common/forms/TextInputFormik";

function LoginForm() {
    const { userStore } = useStore();

    const initialValues = {
        email: '',
        password: '',
        error: null
    }

    return (
        <Container >
            <Formik
                initialValues={initialValues}
                onSubmit={(values, { setErrors }) => userStore.login(values).catch(() =>
                    setErrors({ error: 'Invalid email or password' })
                )
                }
            >
                {({ handleSubmit, isSubmitting, errors }) => (
                    <Form
                        className='ui form'
                        onSubmit={handleSubmit}
                        autoComplete='off'
                    >
                        <Header as='h2' content='Login' color="grey" textAlign="center" />

                        <TextInputFormik placeholders='Email' name='email' label='Email' type="email" />
                        <TextInputFormik placeholders='Password' name='password' label='Password' type="password" />

                        <ErrorMessage name='error' render={() =>
                            <Label style={{ marginBottom: 10 }} basic color='red' content={errors.error}
                            />}
                        />

                        <Button
                            loading={isSubmitting}
                            content='Login'
                            type='submit'
                            fluid
                            className="submitBtnColor Border"
                            positive />
                    </Form>
                )}

            </Formik>
        </Container>
    );
}

export default observer(LoginForm);