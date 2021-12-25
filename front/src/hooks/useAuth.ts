import {useEffect, useLayoutEffect, useState} from "react";

import { useNavigate } from "react-router-dom";

import axios, { AxiosError } from "axios";
import {API_URL} from "../constants";
import {useLocation} from "react-router";
import Services from "../services/services";


const useAuth = () => {

    const navigate = useNavigate();

    const location = useLocation();

    const [authed, setAuthed] = useState(false);

    useEffect(() => {

        if (!["/register"].includes(location.pathname)) {
            Services
                .authCheck()
                .then(() => setAuthed(true))
                .catch(e => {
                    const resp = (e as AxiosError).response;
                    if (resp && resp.status === 401) {
                        navigate("/login");
                    }
                })
        }

    }, [location.pathname, location.search]);

    return authed;
};

export default useAuth;
