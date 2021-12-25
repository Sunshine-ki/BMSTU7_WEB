import axios from "axios";
import { API_URL } from "../constants";

export default class Services {
    static async getTasks() {
        const res = await axios.get(`${API_URL}/tasks`, { withCredentials: true });

        if (res.status === 200) {

            if (Array.isArray(res.data)) {
                return res.data;
            }

        }

        return [];
    }
}
