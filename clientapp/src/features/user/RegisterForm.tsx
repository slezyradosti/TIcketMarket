import { Button, Container, FormField, FormGroup, Header, Label, Radio } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react";
import { ErrorMessage, Field, Form, Formik } from "formik";
import * as Yup from 'yup';
import TextInputFormik from "../../app/common/forms/TextInputFormik";
import DateInputFormik from "../../app/common/forms/DateInputFormik";
import RadioButtonInputFormik from "../../app/common/forms/RadioButtonInputFormik";

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
                // onSubmit={(values, { setErrors }) => userStore.register(values).catch((error) => {
                //     console.log('catched error: ' + error);
                //     setErrors({ error })
                // }
                // )}
                onSubmit={(values, { setErrors }) => console.log(values)}
            >
                {({ handleSubmit, isSubmitting, errors, values, handleChange }) => (
                    <Form
                        className='ui form'
                        onSubmit={handleSubmit}
                        autoComplete='off'
                    >
                        <Header as='h2' content='Register' color="grey" textAlign="center" />

                        <TextInputFormik placeholders='Username' name='username' label='Username' />
                        <TextInputFormik placeholders='Firstname' name='firstname' label='Firstname' />
                        <TextInputFormik placeholders='Lastname' name='lastname' label='Lastname' />
                        <Label>Date of birth</Label>
                        <DateInputFormik placeholderText='Date of birth' name='DOB' dateFormat={'MMMM d, yyyy'} />
                        <TextInputFormik placeholders='Phone' name='phone' label='Phone' />
                        <TextInputFormik placeholders='Email' name='email' label='Email' type="email" />

                        <FormField>
                            <FormGroup inline>
                                <RadioButtonInputFormik name={"isCustomer"} value={"true"} label={"Customer"} />
                                <RadioButtonInputFormik name={"isCustomer"} value={"false"} label={"Seller"} />
                            </FormGroup>
                        </FormField>


                        <TextInputFormik placeholders='Password' name='password' label='Password' type="password" />

                        <ErrorMessage name='error' render={() =>
                            <Label style={{ marginBottom: 10 }} basic color='red' content={errors.error}
                            />}
                        />

                        <Button
                            loading={isSubmitting}
                            content='Register'
                            type='submit'
                            fluid
                            className="submitBtnColor Border"
                            positive />
                    </Form>
                )}

            </Formik>
        </Container >
    );
}

export default observer(LoginForm);