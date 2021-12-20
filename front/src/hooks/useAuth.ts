import {useLayoutEffect, useState} from "react";

import { useNavigate } from "react-router-dom";

import axios, { AxiosError } from "axios";
import {API_URL} from "../constants";


const useAuth = () => {

    const navigate = useNavigate();

    const [authed, setAuthed] = useState(false);

    useLayoutEffect(() => {

        async function sendCheckResponse() {
            try {
                await axios.get(`${API_URL}/check`, { withCredentials: true });
                setAuthed(true)

            } catch (e) {
                if (e) {
                    const resp = (e as AxiosError).response;
                    if (resp && resp.status === 401) {
                        navigate("/login");
                    }
                }
                setAuthed(false)
            }
        }

        sendCheckResponse().then();

    }, []);

    return authed;
};

export default useAuth;
