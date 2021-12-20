import {useEffect, useLayoutEffect, useState} from "react";

import { useNavigate } from "react-router-dom";

import axios, { AxiosError } from "axios";
import {API_URL} from "../constants";
import {useLocation} from "react-router";


const useAuth = () => {

    const navigate = useNavigate();

    const location = useLocation();

    const [authed, setAuthed] = useState(false);

    useEffect(() => {

        async function sendCheckResponse() {
            try {

                if (location.pathname !== "/stats") {
                    await axios.get(`${API_URL}/check`, { withCredentials: true });
                    setAuthed(true)
                }

            } catch (e) {
                if (e) {
                    const resp = (e as AxiosError).response;
                    if (resp && resp.status === 401) {
                        navigate("/login", { replace: true });
                    }
                }
                setAuthed(false)
            }
        }

        sendCheckResponse().then();

    }, [location.pathname]);

    return authed;
};

export default useAuth;
