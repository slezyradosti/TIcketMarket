import { useField } from "formik";
import { FormField, Label } from "semantic-ui-react";

interface Props {
    placeholders: string;
    name: string;
    label?: string;
}

function TextInputFormik(props: Props) {
    const [field, meta] = useField(props.name);

    return (
        <FormField error={meta.touched && !!meta.error}>
            <Label>{props.label}</Label>
            <input {...field} {...props} />
            {meta.touched && meta.error ? (
                <Label basic color='red'>{meta.error}</Label>
            ) : null}
        </FormField>
    )
}

export default TextInputFormik