import React, {useEffect, useState} from "react";

import TaskItem from "./TaskItem/TaskItem";
import {TaskProps} from "../../models/ui/TaskProps";
import Services from "../../services/services";
import {TaskResponse} from "../../models/network/TaskResponse";
import Mapper from "../../services/mapper";

// Логика работы с сетью в отдельный класс. ok.
// ДТО-шки для тех моделей, которые ходят по сети и которые юзаем на фронте. ok.

const TaskList : React.FC = () => {

    const [tasks, setTasks] = useState(new Array<TaskProps>())

    useEffect(() => {



        Services.getTasks().then((e: Array<TaskResponse>) => {
            setTasks(Mapper.mapTasks(e));
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
