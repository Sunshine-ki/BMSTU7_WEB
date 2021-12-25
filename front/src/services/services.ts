import axios, {AxiosResponse} from "axios";
import {API_URL} from "../constants";
import {TaskProps} from "../components/TaskList/TaskProps";
import {StatRowProps} from "../components/Stats/StatRowProps";

export default class Services {
    static async getTasks(): Promise<Array<TaskProps>> {
        const res = await axios.get(`${API_URL}/tasks`, { withCredentials: true });

        if (res.status === 200) {

            if (Array.isArray(res.data)) {
                return res.data;
            }

        }

        return [];
    }

    static async getTaskInfo(id: string | undefined): Promise<TaskProps> {
        const res = await axios.get(`${API_URL}/task/${id}`, { withCredentials: true });

        if (res.status === 200) {
            return res.data;
        }

        return {} as TaskProps;
    }

    static async updateTaskSolution(solution: string, id: string | undefined): Promise<AxiosResponse> {
        return await axios.post(`${API_URL}/task/${id}`, {solution: solution}, {withCredentials: true});
    }

    static async getStats(): Promise<Array<StatRowProps>> {
        const res = await axios.get(`${API_URL}/statistics`, {withCredentials: true});

        if (res.status === 200) {
            return res.data;
        }

        return [];

    }

    static async login(email: string, password: string): Promise<AxiosResponse> {
        return await axios.post(`${API_URL}/login`, { email, password }, { withCredentials: true });
    }

    static async register(email: string, password: string, login: string, name: string, surname: string): Promise<AxiosResponse> {
        return await axios.post(`${API_URL}/registration`, { email, password, login, name, surname }, { withCredentials: true });
    }

    static async logout(searchString: string): Promise<AxiosResponse> {
        const params = new URLSearchParams(location.search);
        if (params.get("logout")) {
            return await axios.get(`${API_URL}/logout`, { withCredentials: true });
        }

        return {} as AxiosResponse;
    }
}
