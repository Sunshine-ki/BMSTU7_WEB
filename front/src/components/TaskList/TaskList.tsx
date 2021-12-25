import React, {useEffect, useState} from "react";

import TaskItem from "./TaskItem/TaskItem";
import {TaskProps} from "./TaskProps";
import axios from "axios";
import {API_URL} from "../../constants";

// Логика работы с сетью в отдельный класс
// ДТО-шки для тех моделей, которые ходят по сети и которые юзаем на фронте

const TaskList : React.FC = () => {

    const [tasks, setTasks] = useState(new Array<TaskProps>())

    useEffect(() => {
        async function fetchTasks() {
            const res = await axios.get(`${API_URL}/tasks`, { withCredentials: true });

            if (res.status === 200) {

                if (Array.isArray(res.data)) {
                    setTasks(res.data)
                }

            }
        }

        fetchTasks().then();
    }, [])

    return (
        <div className="container flex flex-col lg mx-auto">

            { tasks.map((e) => (
                <TaskItem {...e}/>
            )) }

        </div>
    )
}

export default TaskList;
