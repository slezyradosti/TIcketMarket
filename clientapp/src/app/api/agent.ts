import axios, { AxiosError, AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { store } from '../stores/store';
import { router } from '../router/routes';

axios.defaults.baseURL = 'https://localhost:7177';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    });
}

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;

    return config;
})

axios.interceptors.response.use(async response => {
    if (import.meta.env.DEV) await sleep(1000);

    // TODO pagination
    const pagination = response.headers['pagination'];
}, (error: AxiosError) => {
    const { data, status, config } = error.response as AxiosResponse;

    switch (status) {
        case 400:
            if (config.method === 'get' && data.errors.hasOwnPropery('id')) {
                router.navigate('/not-found');
            }
            if (data.error) {
                const modalStateErrors = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateErrors.push(data.errors[key]);
                    }
                }

                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('unauthorised');
            break;
        case 403:
            toast.error('forbidden');
            break;
        case 404:
            router.navigate('/not-found');
            break;
        case 500:
            store.commonStore.setServerError(data);
            router.navigate('/server-error');
            break;
    }
    return Promise.reject(error);
})


const responseBody = <T>(reponse: AxiosResponse<T>) => reponse.data; // get responseData from http response

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: object) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Events = {
    list: () => requests.get<>('/list')
}

const agent = {
    Events
}

export default agent;