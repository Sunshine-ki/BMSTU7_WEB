import {TaskProps} from "../models/ui/TaskProps";
import {TaskResponse} from "../models/network/TaskResponse";


export default class Mapper {

    static mapTask(taskResponse: TaskResponse): TaskProps {
        return {
            id: taskResponse.id,
            name: taskResponse.name,
            shortDescription: taskResponse.shortDescription,
            detailedDescription: taskResponse.detailedDescription,
            solution: taskResponse.solution,
            tableName: taskResponse.tableName,
            authorId: taskResponse.authorId,
            done: taskResponse.done,
        }
    }

}
