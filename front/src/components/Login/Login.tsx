import React, {useEffect} from "react";

import { Formik } from 'formik';

import axios, {AxiosError, AxiosResponse} from "axios";
import {API_URL} from "../../constants";
import {useLocation, useNavigate} from "react-router";
import Services from "../../services/services";

const Login: React.FC = () => {

    const navigate = useNavigate();

    const location = useLocation();

    useEffect(() => {

        Services.logout(location.search).then((e: AxiosResponse) => {
            if (e.status === 200) {
                navigate("/login", { replace: true });
            }
        })
    }, [])

    return(
        <div className="container lg mx-auto login-container mt-12">
            <Formik
                initialValues={{ email: '', password: '' }}
                validate={values => {
                    const errors = { email: "", password: '' };
                    if (!values.email) {
                        errors.email = 'Обязательно';
                    } else if (
                        !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)
                    ) {
                        errors.email = 'Неправильный формат почты';
                    }

                    if (!values.password) {
                        errors.password = "Обязательно";
                    }

                    if (errors.email || errors.password) {
                        return errors;
                    }
                }}
                onSubmit={async (values, { setSubmitting, setErrors }) => {
                    setSubmitting(true);

                    try {
                        await Services.login({ email: values.email, password: values.password });
                        navigate("/tasks");
                        setSubmitting(false)
                    } catch (e) {
                        if (e) {
                            const resp = (e as AxiosError).response;
                            if (resp && resp.status === 418) {
                                setErrors({email: resp.data["Title"]})
                            }
                        }
                    }

                }}
            >
                {({
                      values,
                      errors,
                      touched,
                      handleChange,
                      handleBlur,
                      handleSubmit,
                      isSubmitting,
                      /* and other goodies */
                  }) => (
                    <form onSubmit={handleSubmit} className="flex flex-col w-4/5 md:w-2/5 mx-auto">
                        <div className="flex justify-between mb-4">
                            <label htmlFor="email">Email: </label>
                            <span className="text-red-500">{errors.email && touched.email && errors.email}</span>
                        </div>
                        <input
                            id="email"
                            type="email"
                            name="email"
                            className="p-4 border-blue-800 focus:outline-none border-2 h-8 transition-all"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values.email}
                        />

                        <div className="flex justify-between mt-4 mb-4">
                            <label htmlFor="password">Пароль: </label>
                            <span className="text-red-500">{errors.password && touched.password && errors.password}</span>
                        </div>
                        <input
                            id="password"
                            type="password"
                            name="password"
                            className="p-4 border-blue-800 focus:outline-none border-2 h-8 transition-all"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values.password}
                        />

                        <button type="submit" className="mt-4 w-1/4 transition-colors hover:bg-indigo-500 bg-indigo-800 text-white h-8 mt-8 self-center">
                            Вход
                        </button>
                    </form>
                )}
            </Formik>
        </div>
    )

}


export default Login;
