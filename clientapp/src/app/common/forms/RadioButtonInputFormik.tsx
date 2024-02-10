import { Field, useField } from "formik";

interface Props {
    name: string;
    value: string;
    label: string;
}

function RadioButtonInputFormik(props: Props) {
    const [field, meta, helpers] = useField(props.name);

    return (
        <div className="field">
            <div className="ui radio checkbox">
                <Field
                    className='ui radio checkbox'
                    type="radio"
                    name={props.name}
                    value={props.value}
                />
                <label>{props.label}</label>
            </div>
        </div>
    )
}

export default RadioButtonInputFormik