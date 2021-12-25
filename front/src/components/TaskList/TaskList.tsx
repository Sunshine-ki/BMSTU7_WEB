import React, {useEffect, useState} from "react";

import TaskItem from "./TaskItem/TaskItem";
import {TaskProps} from "./TaskProps";
import Services from "../../services/services";

// Логика работы с сетью в отдельный класс
// ДТО-шки для тех моделей, которые ходят по сети и которые юзаем на фронте

const TaskList : React.FC = () => {

    const [tasks, setTasks] = useState(new Array<TaskProps>())

    useEffect(() => {

        Services.getTasks().then(e => {
            setTasks(e);
        })


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
