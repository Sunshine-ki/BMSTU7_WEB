import React from "react";

import { Formik } from 'formik';
import axios from "axios";
import {API_URL} from "../../constants";
import {useNavigate} from "react-router";

const Register: React.FC = () => {

    const navigate = useNavigate();

    return(
        <div className="container lg mx-auto register-container mt-12">
            <Formik
                initialValues={{ email: '', password: '', name: '', surname: '' }}
                validate={values => {
                    const errors = { email: "", password: '', name: '', surname: '' };
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

                    if (!values.name) {
                        errors.name = "Обязательно";
                    }

                    if (!values.surname) {
                        errors.surname = "Обязательно";
                    }

                    if (errors.email || errors.password || errors.name || errors.surname) {
                        return errors;
                    }
                }}
                onSubmit={async (values, { setSubmitting }) => {
                    setSubmitting(true);
                    const res = await axios.post(`${API_URL}/registration`, { email: values.email, password: values.password, name: values.name, surname: values.surname }, { withCredentials: true });

                    if (res.status === 200) {
                        navigate("/tasks");
                        setSubmitting(false)
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

                        <div className="flex justify-between mt-4 mb-4">
                            <label htmlFor="name">Имя: </label>
                            <span className="text-red-500">{errors.name && touched.name && errors.name}</span>
                        </div>
                        <input
                            id="name"
                            type="text"
                            name="name"
                            className="p-4 border-blue-800 focus:outline-none border-2 h-8 transition-all"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values.name}
                        />

                        <div className="flex justify-between mt-4 mb-4">
                            <label htmlFor="surname">Фамилия: </label>
                            <span className="text-red-500">{errors.surname && touched.surname && errors.surname}</span>
                        </div>
                        <input
                            id="surname"
                            type="text"
                            name="surname"
                            className="p-4 border-blue-800 focus:outline-none border-2 h-8 transition-all"
                            onChange={handleChange}
                            onBlur={handleBlur}
                            value={values.surname}
                        />

                        <div className="flex justify-between mt-4 mb-4">
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

                        <button type="submit" disabled={isSubmitting} className="mt-4 w-1/3 transition-colors hover:bg-indigo-500 bg-indigo-800 text-white h-8 mt-8 self-center">
                            Регистрация
                        </button>
                    </form>
                )}
            </Formik>
        </div>
    )

}


export default Register;
