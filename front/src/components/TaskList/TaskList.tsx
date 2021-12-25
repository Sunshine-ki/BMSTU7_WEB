import React, {useEffect, useState} from "react";

import TaskItem from "./TaskItem/TaskItem";
import {TaskProps} from "../../models/ui/TaskProps";
import Services from "../../services/services";
import {TaskResponse} from "../../models/network/TaskResponse";

// Логика работы с сетью в отдельный класс
// ДТО-шки для тех моделей, которые ходят по сети и которые юзаем на фронте

const TaskList : React.FC = () => {

    const [tasks, setTasks] = useState(new Array<TaskProps>())

    useEffect(() => {



        Services.getTasks().then((e: Array<TaskResponse>) => {
            setTasks(e.map(e => ({
                id: e.id,
                name: e.name,
                shortDescription: e.shortDescription,
                detailedDescription: e.detailedDescription,
                solution: e.solution,
                tableName: e.tableName,
                authorId: e.authorId,
                done: e.done,
            })));
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
