import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { useEffect, useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import { Formik, Form } from "formik";
import * as Yup from 'yup';
import LoadingComponent from "../../../app/layout/LoadingComponent";
import TextInputFormik from "../../../app/common/forms/TextInputFormik";
import { useNavigate, useParams } from "react-router-dom";
import { Link } from "react-router-dom";
import { TicketDiscountFormValues } from "../../../app/models/forms/ticketDiscountFormValues";

function TicketDiscountSellersForm() {
    const { ticketDiscountStore } = useStore();
    const { id } = useParams();
    const navigate = useNavigate()

    const [initialState, setInitialState] = useState<TicketDiscountFormValues>(new TicketDiscountFormValues());

    useEffect(() => {
        if (id) ticketDiscountStore.details(id).then(tDiscount => setInitialState(new TicketDiscountFormValues(tDiscount)))
    }, [ticketDiscountStore, id])


    const validationSchema: TicketDiscountFormValues = Yup.object({
        discountPercentage: Yup.number().integer().positive().required('Discount Percentage is required'),
        code: Yup.string().required('Code is required'),
    }).required()

    function handleFormSubmit(tDiscount: TicketDiscountFormValues) {
        console.log('handleFormSubmit: ' + tDiscount)
        if (!tDiscount.id) {
            ticketDiscountStore.createOne(tDiscount).then(() => navigate(`/TicketDiscount/my-discounts`));
        } else {
            ticketDiscountStore.editOne(tDiscount).then(() => navigate(`/TicketDiscount/my-discounts`));
            //updateActivity(activity).then(() => navigate(`/activities/${activity.id}`))
        }
    }

    if (ticketDiscountStore.loadingInitial) {
        return <LoadingComponent content='Loading app...' />
    }

    return (
        <>
            <Segment clearing>
                <Header>Create Event</Header>

                <Formik
                    enableReinitialize
                    initialValues={initialState}
                    validationSchema={validationSchema}
                    onSubmit={(values) => handleFormSubmit(values)}

                >
                    {({ handleSubmit, isSubmitting, dirty, isValid, values }) => (
                        <Form onSubmit={handleSubmit} className="ui form" autoComplete='off'>
                            <TextInputFormik placeholders='15' name='discountPercentage' label='Discount Percentage' />
                            <TextInputFormik placeholders='code' name='code' label='Code' />

                            <Button
                                loading={isSubmitting}
                                disabled={isSubmitting || !dirty || !isValid}
                                floated="right"
                                type='submit'
                                content='Submit'
                                positive
                            />
                            <Button as={Link} to='/TicketDiscount/my-discounts' floated='right' type='button' content='Cancel' className="cancelBtnColor" />
                        </Form>
                    )}
                </Formik >
            </Segment>
        </>
    );
}

export default observer(TicketDiscountSellersForm);
