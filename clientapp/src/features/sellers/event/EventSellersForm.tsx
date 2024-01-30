import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { ChangeEvent, useEffect, useState } from "react";
import { Event } from "../../../app/models/tables/event";
import { Button, Form, FormField, Header, Input, Select } from "semantic-ui-react";
import { Formik } from "formik";
import * as Yup from 'yup';
import LoadingComponent from "../../../app/layout/LoadingComponent";

function EventSellersForm() {
    const { eventSellersStore, eventCategoryStore, eventTypeStore } = useStore();
    const { createOne, closeForm, loading, selectedElement } = eventSellersStore;

    useEffect(() => {
        eventCategoryStore.loadList();
        eventTypeStore.loadList();

    }, [eventCategoryStore, eventTypeStore])

    const initialState: Event = {
        title: "",
        category: null,
        categoryId: "",
        description: "",
        place: "",
        date: undefined,
        user: null,
        userId: "",
        type: null,
        typeId: "",
        moderator: "",
        totalPlaces: 0,
        id: selectedElement?.id || '',
        createdAt: null,
        updatedAt: null
    };

    const validationSchema = Yup.object({
        title: Yup.string().required('This field is required'),
        category: Yup.object().required().nonNullable(),
        description: Yup.string().required('This field is required'),
        place: Yup.string().required('This field is required'),
        date: Yup.date().required(),
        type: Yup.object().required().nonNullable(),
        moderator: Yup.string().required('This field is required'),
        totalPlaces: Yup.number().required('This field is required. Minimum values is 0').min(0)
    })

    const [eventDto, setEventDto] = useState(initialState);

    function handleSubmit() {
        createOne(eventDto);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        setEventDto({ ...eventDto, [name]: value });
    }

    if (eventCategoryStore.loadingInitial || eventTypeStore.loadingInitial || eventSellersStore.loadingInitial) {
        return <LoadingComponent content='Loading app...' />
    }

    return (
        <>
            <Header>Create Event</Header>

            <Formik
                initialValues={initialState}
                validationSchema={validationSchema}
                onSubmit={(values, { setErrors }) => eventSellersStore.createOne(values)}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty, handleChange }) => (
                    <Form onSubmit={handleSubmit} autoComplete='off'>
                        <FormField
                            required={true}
                            autoFocus
                            placeholder='Title'
                            value={eventDto.title}
                            name='title'
                            onChange={handleInputChange}
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
                            onChange={handleChange('category')}
                        />
                        <FormField
                            required={true}
                            placeholder='Description'
                            value={eventDto.description}
                            name='description'
                            onChange={handleInputChange}
                            fluid
                            label='Description'
                            control={Input}
                        />
                        <FormField
                            required={true}
                            placeholder='Place'
                            value={eventDto.place}
                            name='place'
                            onChange={handleInputChange}
                            fluid
                            label='Place'
                            control={Input}
                        />
                        <FormField
                            required={true}
                            placeholder='Date'
                            value={eventDto.date}
                            name='date'
                            onChange={handleInputChange}
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
                            onChange={handleInputChange}
                            fluid
                            label='Type'
                            control={Select}
                            options={eventTypeStore.typeOptions}
                        />
                        <FormField
                            required={true}
                            placeholder='Moderator'
                            value={eventDto.moderator}
                            name='moderator'
                            onChange={handleInputChange}
                            fluid
                            label='Moderator'
                            control={Input}
                        />
                        <FormField
                            required={true}
                            placeholder='Total Places'
                            value={eventDto.totalPlaces}
                            name='totalPlaces'
                            onChange={handleInputChange}
                            fluid
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
