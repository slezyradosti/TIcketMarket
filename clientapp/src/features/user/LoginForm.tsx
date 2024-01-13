import { Button, Container, Header, Label } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react";
import { ErrorMessage, Field, Form, Formik } from "formik";
import * as Yup from 'yup';

function LoginForm() {
    const { userStore } = useStore();

    const initialValues = {
        email: '',
        password: '',
        error: null
    }

    const validationSchema = Yup.object({
        email: Yup.string().email()
    });

    return (
        <Container >
            <Formik
                validationSchema={validationSchema}
                initialValues={initialValues}
                onSubmit={(values, { setErrors }) => userStore.login(values).catch(() =>
                    setErrors({ error: 'Invalid email or password' }))}
            >
                {({ handleSubmit, isSubmitting, errors }) => (
                    <Form
                        className='ui form'
                        onSubmit={handleSubmit}
                        autoComplete='off'
                    >
                        <Header as='h2' content='Login' color="grey" textAlign="center" />
                        <div className="ui form field">
                            <Field
                                required={true}
                                placeholder='Email'
                                name='email'
                            />
                        </div>
                        <div className="ui form field">
                            <Field
                                required={true}
                                placeholder='Password'
                                name='password'
                                type='password'
                            />
                        </div>
                        <ErrorMessage name='error' render={() =>
                            <Label style={{ marginBottom: 10 }} basic color='red' content={errors.error}
                            />}
                        />
                        <Button
                            loading={isSubmitting}
                            content='Login'
                            type='submit'
                            fluid
                            className="submitBtnColor Border" />
                    </Form>
                )}

            </Formik>
        </Container>
    );
}

export default observer(LoginForm);