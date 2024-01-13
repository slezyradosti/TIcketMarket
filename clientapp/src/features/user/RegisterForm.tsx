import { Button, Container, Header, Label } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react";
import { ErrorMessage, Field, Form, Formik } from "formik";
import * as Yup from 'yup';

function LoginForm() {
    const { userStore } = useStore();

    const phoneRegExp = /^((\+[1-9]{1,4}[ -]?)|(\([0-9]{2,3}\)[ -]?)|([0-9]{2,4})[ -]?)*?[0-9]{3,4}[ -]?[0-9]{3,4}$/

    const initialValues = {
        email: "",
        password: "",
        username: "",
        firstname: "",
        lastname: "",
        DOB: new Date(2000, 0, 1),
        phone: "",
        isCustomer: true,
        error: null
    }

    const validationSchema = Yup.object({
        email: Yup.string().email(),
        phone: Yup.string().matches(phoneRegExp, 'Phone number is not valid')
    });

    return (
        <Container >
            <Formik
                validationSchema={validationSchema}
                initialValues={initialValues}
                onSubmit={(values, { setErrors }) => userStore.register(values).catch((error) => {
                    console.log('catched error: ' + error);
                    setErrors({ error })
                }
                )}
            >
                {({ handleSubmit, isSubmitting, errors }) => (
                    <Form
                        className='ui form'
                        onSubmit={handleSubmit}
                        autoComplete='off'
                    >
                        <Header as='h2' content='Register' color="grey" textAlign="center" />
                        <div className="ui form field">
                            <Field
                                required={true}
                                placeholder='Username'
                                name='username'
                            />
                        </div>
                        <div className="ui form field">
                            <Field
                                required={true}
                                placeholder='Firstname'
                                name='firstname'
                            />
                        </div>
                        <div className="ui form field">
                            <Field
                                required={true}
                                placeholder='Lastname'
                                name='lastname'
                            />
                        </div>
                        <div className="ui form field">
                            <Field
                                required={true}
                                placeholder='Date of birth'
                                name='DOB'
                            />
                        </div>
                        <div className="ui form field">
                            <Field
                                required={true}
                                placeholder='Phone'
                                name='phone'
                            />
                        </div>
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
                                placeholder='Rights (seller or customers)'
                                name='isCustomer'
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
                            content='Register'
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