import React from "react";
import { TaskProps } from "../TaskProps";
import { useNavigate } from "react-router-dom";

const TaskItem: React.FC<TaskProps> = (props) => {


    const navigate = useNavigate()

    return (
        <div onClick={ () => navigate(`/task/${props.id}`) } className="container group cursor-pointer transition-colors hover:bg-indigo-600 hover:text-white flex mb-8 mx-auto w-3/4 md:w-2/5  bg-white shadow-md flex justify-around items-center h-20 text-black">
            <span className="">{ props.name }</span>
            <span>{ props.shortDescription }</span>
            <span className={ props.done ? "text-green-500 group-hover:text-white " : "text-red-500 group-hover:text-white " }>{ props.done ? "Сделано" : "Не сделано" }</span>
        </div>
    )
}


export default TaskItem;
