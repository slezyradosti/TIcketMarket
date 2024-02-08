import { observer } from "mobx-react";
import { useStore } from "../../../app/stores/store";
import { useEffect, useState } from "react";
import { Button, Header, Segment } from "semantic-ui-react";
import { Formik, Form } from "formik";
import * as Yup from 'yup';
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useNavigate, useParams } from "react-router-dom";
import { Link } from "react-router-dom";
import { TableEventFormValues } from "../../../app/models/forms/tableEventFormValues";
import SelectInputFormik from "../../../app/common/forms/SelectInputFormik";
import EventTableSellersDetails from "../eventTable/eventTableSellersDetails";
import EventInlineDetails from "../event/EventInlineDetails";

function TableEventSellersForm() {
    const { tableEventStore, eventSellersStore, eventTableStore } = useStore();
    const { id } = useParams();
    const navigate = useNavigate()

    const [initialState, setInitialState] = useState<TableEventFormValues>(new TableEventFormValues());

    useEffect(() => {
        eventSellersStore.loadOptions();
        eventTableStore.loadOptions();

        if (id) tableEventStore.details(id).then(tableEvent => setInitialState(new TableEventFormValues(tableEvent)))

    }, [eventSellersStore, eventTableStore, id])

    const validationSchema: TableEventFormValues = Yup.object({
        eventId: Yup.string().required('Event is required'),
        tableId: Yup.string().required('Table is required'),
    }).required()

    function handleFormSubmit(tableEvent: TableEventFormValues) {
        if (!tableEvent.id) {
            tableEventStore.createOne(tableEvent).then(() => navigate(`/TableEvent/my-list`));
        } else {
            tableEventStore.editOne(tableEvent).then(() => navigate(`/TableEvent/my-list`));
        }
    }

    if (eventSellersStore.eventOptionsLoading || eventTableStore.eventTableOptionsLoading) {
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
                            <SelectInputFormik placeholder='Event' name='eventId' options={eventSellersStore.eventOptions} label="Event" />

                            {values.eventId != '' ? (
                                <>
                                    <EventInlineDetails eventId={values.eventId} />
                                </>)
                                : (null)
                            }
                            <br />
                            <SelectInputFormik placeholder='Table' name='tableId' options={eventTableStore.eventTableOptions} label="Table number" />

                            {values.tableId != '' ? (
                                <>
                                    <EventTableSellersDetails tableId={values.tableId} />
                                </>)
                                : (null)
                            }

                            <Button
                                loading={isSubmitting}
                                disabled={isSubmitting || !dirty || !isValid}
                                floated="right"
                                type='submit'
                                content='Submit'
                                positive
                            />
                            <Button as={Link} to='/TableEvent/my-list' floated='right' type='button' content='Cancel' className="cancelBtnColor" />
                        </Form>
                    )}
                </Formik >
            </Segment >
        </>
    );
}

export default observer(TableEventSellersForm);
