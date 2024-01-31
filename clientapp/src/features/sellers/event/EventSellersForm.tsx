import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { ChangeEvent, useEffect, useState } from "react";
import { Event } from "../../../app/models/tables/event";
import { Button, Form, FormField, Header, Input, Select } from "semantic-ui-react";
import { Formik, useField } from "formik";
import * as Yup from 'yup';
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { EventFormValues } from "../../../app/models/forms/EventFormValues";

function EventSellersForm() {
    const { eventSellersStore, eventCategoryStore, eventTypeStore } = useStore();
    const { createOne, closeForm, loading, selectedElement } = eventSellersStore;

    useEffect(() => {
        eventCategoryStore.loadList();
        eventTypeStore.loadList();

    }, [eventCategoryStore, eventTypeStore])

    const [initialState, setInitialState] = useState<EventFormValues>(new EventFormValues());

    const validationSchema = Yup.object({
        // title: Yup.string().required('This field is required'),
        // category: Yup.object().required().nonNullable(),
        // description: Yup.string().required('This field is required'),
        // place: Yup.string().required('This field is required'),
        // date: Yup.date().required(),
        // type: Yup.object().required().nonNullable(),
        // moderator: Yup.string().required('This field is required'),
        // totalPlaces: Yup.number().required('This field is required. Minimum values is 0').min(0)
    })

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setInitialState({ ...initialState, [name]: value });
    }

    function handleSelectInputChange(event, data) {
        const { value, name } = data;

        switch (name) {
            case 'category':
                setInitialState({ ...initialState, categoryId: value });
                break;
            case 'type':
                setInitialState({ ...initialState, typeId: value });
                break;
            default:
                break;
        }
    }

    if (eventCategoryStore.loadingInitial || eventTypeStore.loadingInitial || eventSellersStore.loadingInitial) {
        return <LoadingComponent content='Loading app...' />
    }

    function handleFormSubmit() {
        if (!initialState.id) {
            console.log(initialState);
            // let newEvent = {
            //     ...initialState,
            //     typeId: initialState.type!.id,
            //     categoryId: initialState.category!.id
            // }

            eventSellersStore.createOne(initialState);
        } else {
            eventSellersStore.editOne(initialState);
        }
    }

    return (
        <>
            <Header>Create Event</Header>

            <Formik
                enablereinitialize={true}
                initialValues={initialState}
                validationSchema={validationSchema}
                onSubmit={() => handleFormSubmit()}

            >
                {({ handleSubmit, isValid, isSubmitting, dirty, handleChange }) => (
                    <Form onSubmit={handleSubmit} autoComplete='off'>
                        <FormField
                            required={true}
                            autoFocus
                            placeholder='Title'
                            value={initialState.title}
                            name='title'
                            id='title'
                            onChange={e => handleInputChange(e)}
                            fluid
                            label='Title'
                            control={Input}
                        />
                        <FormField
                            required={true}
                            value={eventSellersStore.selectedElement?.category?.category}
                            name='category'
                            fluid
                            label='Category'
                            control={Select}
                            options={eventCategoryStore.categoryOptions}
                            // onChange={(e) => setFieldValue("category", e.target.value)}
                            onChange={handleSelectInputChange}
                        />
                        <FormField
                            required={true}
                            placeholder='Description'
                            value={initialState.description}
                            name='description'
                            onChange={e => handleInputChange(e)}
                            fluid
                            label='Description'
                            control={Input}
                        />
                        <FormField
                            required={true}
                            placeholder='Place'
                            value={initialState.place}
                            name='place'
                            onChange={e => handleInputChange(e)}
                            fluid
                            label='Place'
                            control={Input}
                        />
                        <FormField
                            required={true}
                            placeholder='Date'
                            value={initialState.date}
                            name='date'
                            onChange={e => handleInputChange(e)}
                            fluid
                            label='Date'
                            type='date'
                            control={Input}
                        />
                        <FormField
                            required={true}
                            placeholder='Type'
                            value={eventSellersStore.selectedElement?.type?.type}
                            name='type'
                            onChange={handleSelectInputChange}
                            fluid
                            label='Type'
                            control={Select}
                            options={eventTypeStore.typeOptions}
                        />
                        <FormField
                            required={true}
                            placeholder='Moderator'
                            value={initialState.moderator}
                            name='moderator'
                            onChange={e => handleInputChange(e)}
                            fluid
                            label='Moderator'
                            control={Input}
                        />
                        <FormField
                            required={true}
                            placeholder='Total Places'
                            value={initialState.totalPlaces}
                            name='totalPlaces'
                            onChange={e => handleInputChange(e)}
                            fluid
                            type="number"
                            label='Total Places'
                            control={Input}
                        />
                        <Button loading={isSubmitting} floated="right" type='submit' content='Submit' positive />
                        <Button onClick={closeForm} floated='right' type='button' content='Cancel' className="cancelBtnColor" />
                    </Form>
                )}
            </Formik >
        </>
    );
}

export default observer(EventSellersForm);
