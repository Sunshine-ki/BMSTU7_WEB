import React, {useEffect, useState} from "react";
import { TaskProps } from "../../models/ui/TaskProps";
import {API_URL} from "../../constants";
import {useNavigate, useParams} from "react-router";
import {Helmet} from "react-helmet";

import axios from "axios";
import {Formik} from "formik";
import Services from "../../services/services";

const Task : React.FC = () => {

    const [showFullDescription, setFullDescription] = useState(false);

    const params = useParams();
    const navigate = useNavigate();

    const [taskInfo, setTaskInfo] = useState({} as TaskProps)
    const [title, setTitle] = useState("")

    const handleFullDescription = () => {
        setFullDescription(!showFullDescription);
    }

    useEffect(() => {

        Services.getTaskInfo(params.id).then(e => {
            setTaskInfo(e);
        })

    }, [])

    return (
        <div className="container flex mx-auto">
            <Helmet>
                <title>Задание №{ params.id }</title>
            </Helmet>
            <div className="flex flex-col mx-auto text-left justify-items-start w-4/5 md:w-1/2 ">
                <div className="flex justify-between w-full">
                    <span className="mb-4">{ taskInfo.name }</span>
                    <span className="mb-4">{ title }</span>
                </div>
                <span className="mb-4">{ taskInfo.shortDescription }</span>

                <button type="button" onClick={ handleFullDescription } className="text-left text-indigo-600">Показать полное описание</button>

                { showFullDescription && (
                    <span>{ taskInfo.detailedDescription }</span>
                ) }

                <Formik
                    initialValues={{ solution: '' }}
                    onSubmit={async (values, { setSubmitting }) => {
                        setSubmitting(true);

                        const res = await Services.updateTaskSolution(values.solution, params.id);

                        if (res.status === 200) {
                            setTitle(res.data["Title"])
                            setSubmitting(false)
                        }

                    }}
                >
                    {({
                          values,
                          handleChange,
                          handleSubmit
                      }) => (
                        <form onSubmit={handleSubmit} className="flex flex-col">
                             <textarea id="solution" onChange={ handleChange } value={ values.solution }  placeholder={ "select *" } rows={ 20 } name="solution" className="font-mono mt-4 p-2 rounded-sm border-indigo-700 focus:outline-none border-2">
                             </textarea>

                            <button type="submit"  className="mt-4 w-1/2 lg:w-1/4 transition-colors hover:bg-indigo-500 bg-indigo-800 text-white h-8 mt-8">
                                Отправить
                            </button>
                        </form>
                    )}
                </Formik>
            </div>

        </div>
    )
}

export default Task;
