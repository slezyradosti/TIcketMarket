import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { useEffect, useState } from "react";
import { Button, Header, Label, Segment } from "semantic-ui-react";
import { Formik, Form } from "formik";
import * as Yup from 'yup';
import LoadingComponent from "../../../app/layout/LoadingComponent";
import TextInputFormik from "../../../app/common/forms/TextInputFormik";
import SelectInputFormik from "../../../app/common/forms/SelectInputFormik";
import DateInputFormik from "../../../app/common/forms/DateInputFormik";
import { useNavigate, useParams } from "react-router-dom";
import { Link } from "react-router-dom";
import { EventFormValues } from "../../../app/models/forms/eventFormValues";

function EventSellersForm() {
    const { eventSellersStore, eventCategoryStore, eventTypeStore } = useStore();
    const { id } = useParams();
    const navigate = useNavigate()

    const [initialState, setInitialState] = useState<EventFormValues>(new EventFormValues());

    useEffect(() => {
        eventCategoryStore.loadList();
        eventTypeStore.loadList();

        if (id) eventSellersStore.detailsSellers(id).then(event => setInitialState(new EventFormValues(event)))
    }, [eventCategoryStore, eventTypeStore])


    const validationSchema: EventFormValues = Yup.object({
        title: Yup.string().required('Title is required'),
        categoryId: Yup.string().required('Category is required'),
        description: Yup.string().required('Description is required'),
        place: Yup.string().required('Place is required'),
        date: Yup.date().required('Date is required'),
        typeId: Yup.string().required('Type is required').nonNullable(),
        moderator: Yup.string().required('Moderator is required'),
        totalPlaces: Yup.number().integer().required('Total places number is required. Minimum values is 0').min(0).max(10000000)
    }).required()

    function handleFormSubmit(event: EventFormValues) {
        if (!event.id) {
            eventSellersStore.createOne(event).then(() => navigate(`/event/my-events`));
        } else {
            eventSellersStore.editOne(event).then(() => navigate(`/event/my-events`));
        }
    }

    if (eventCategoryStore.loadingInitial || eventTypeStore.loadingInitial || eventSellersStore.loadingInitial) {
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
                            <TextInputFormik placeholders='title' name='title' label='Title' />
                            <SelectInputFormik placeholder='Category' name='categoryId' options={eventCategoryStore.categoryOptions} label="Category" />
                            <TextInputFormik placeholders='description' name='description' label='Description' />
                            <TextInputFormik placeholders='place' name='place' label='Place' />

                            <Label>Date</Label>
                            <DateInputFormik placeholderText='date' name='date' dateFormat={'MMMM d, yyyy'} />

                            <SelectInputFormik placeholder='Type' name='typeId' options={eventTypeStore.typeOptions} label="Type" />
                            <TextInputFormik placeholders='moderator' name='moderator' label='Moderator' />
                            <TextInputFormik placeholders='totalPlaces' name='totalPlaces' label='Total Places' />

                            <Button
                                loading={isSubmitting}
                                disabled={isSubmitting || !dirty || !isValid}
                                floated="right"
                                type='submit'
                                content='Submit'
                                positive
                            />
                            <Button as={Link} to='/event/my-events' floated='right' type='button' content='Cancel' className="cancelBtnColor" />
                        </Form>
                    )}
                </Formik >
            </Segment>
        </>
    );
}

export default observer(EventSellersForm);
