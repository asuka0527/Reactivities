import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { history } from "../..";
import { Activity } from "../models/activity";
import { store } from "../stores/store";

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};

axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.response.use(
  async (response) => {
    await sleep(1000);
    return response;
  },
  (error: AxiosError) => {
    // we may not have a response object so add !
    const { data, status, config } = error.response!;

    switch (status) {
      case 400:
        if (typeof data === "string") {
          toast.error(data);
        }
        // bad guid
        if (config.method === "get" && data.errors.hasOwnProperty("id")) {
          history.push("/not-found");
        }

        if (data.errors) {
          const modalStateErrors = [];

          for (const key in data.errors) {
            // object property access syntax []
            if (data.errors[key]) {
              modalStateErrors.push(data.errors[key]);
            }
          }
          // throw it back to the function and flat it so we just get back an array of strings
          throw modalStateErrors.flat();
        }
        break;
      case 401:
        toast.error("unaothorised");
        break;
      case 404:
        history.push("/not-found");
        // toast.error("not found");
        break;
      case 500:
        store.commonStore.setServerError(data);
        history.push("/server-error");
        // toast.error("server error");
        break;

      default:
        break;
    }
    return Promise.reject(error);
  }
);

// adding a generic <T> to AxiosResponse
const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Activities = {
  list: () => requests.get<Activity[]>("/activities"),
  details: (id: string) => requests.get<Activity>(`/activities/${id}`),
  create: (activity: Activity) => axios.post<void>("/activities", activity),
  update: (activity: Activity) =>
    axios.put<void>(`/activities/${activity.id}`, activity),
  delete: (id: string) => axios.delete<void>(`/activities/${id}`),
};

const agent = {
  Activities,
};

export default agent;
