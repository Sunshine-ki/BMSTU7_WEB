import axios, {AxiosResponse} from "axios";
import {API_URL} from "../constants";
import {TaskProps} from "../models/ui/TaskProps";
import {StatRowProps} from "../models/ui/StatRowProps";
import {TaskResponse} from "../models/network/TaskResponse";
import {StatRowResponse} from "../models/network/StatRowResponse";
import {UserRequest} from "../models/network/UserRequest";

export default class Services {
    static async getTasks(): Promise<Array<TaskResponse>> {
        const res = await axios.get(`${API_URL}/tasks`, { withCredentials: true });

        if (res.status === 200) {

            if (Array.isArray(res.data)) {
                return res.data;
            }

        }

        return [];
    }

    static async getTaskInfo(id: string | undefined): Promise<TaskResponse> {
        const res = await axios.get(`${API_URL}/task/${id}`, { withCredentials: true });

        if (res.status === 200) {
            return res.data;
        }

        return {} as TaskProps;
    }

    static async updateTaskSolution(solution: string, id: string | undefined): Promise<AxiosResponse> {
        return await axios.post(`${API_URL}/task/${id}`, {solution: solution}, {withCredentials: true});
    }

    static async getStats(): Promise<Array<StatRowResponse>> {
        const res = await axios.get(`${API_URL}/statistics`, {withCredentials: true});

        if (res.status === 200) {
            return res.data;
        }

        return [];

    }

    static async login(request: UserRequest): Promise<AxiosResponse> {
        return await axios.post(`${API_URL}/login`, request, { withCredentials: true });
    }

    static async register(request: UserRequest): Promise<AxiosResponse> {
        return await axios.post(`${API_URL}/registration`, request , { withCredentials: true });
    }

    static async logout(searchString: string): Promise<AxiosResponse> {
        const params = new URLSearchParams(searchString);
        if (params.get("logout")) {
            return await axios.get(`${API_URL}/logout`, { withCredentials: true });
        }

        return {} as AxiosResponse;
    }

    static async authCheck(): Promise<AxiosResponse> {
        return await axios.get(`${API_URL}/check`, { withCredentials: true });
    }
}
