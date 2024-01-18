export interface User {
    id: string | undefined;
    firstname: string;
    lastname: string;
    dob: Date;
    phone: string;
    email: string;
    token: string | undefined;
}