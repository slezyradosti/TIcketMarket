import axios, { AxiosResponse } from 'axios';

axios.defaults.baseURL = 'https://localhost:7177';

const responseBody = (reponse: AxiosResponse) => reponse.data; // get responseData from http response

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: object) => axios.post(url, body).then(responseBody),
    put: (url: string, body: object) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody)
}

const Events = {
    list: () => requests.get('/list')
}

const agent = {
    Events
}

export default agent;