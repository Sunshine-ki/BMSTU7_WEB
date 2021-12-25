import {TaskProps} from "../models/ui/TaskProps";
import {TaskResponse} from "../models/network/TaskResponse";
import {StatRowResponse} from "../models/network/StatRowResponse";
import {StatRowProps} from "../models/ui/StatRowProps";


export default class Mapper {

    static mapTasks(tasks: Array<TaskResponse>): Array<TaskProps> {
        return tasks.map(el => Mapper.mapTask(el));
    }

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

    static mapStats(tasks: Array<StatRowResponse>): Array<StatRowProps> {
        return tasks.map(el => Mapper.mapStat(el));
    }

    static mapStat(rowResponse: StatRowResponse): StatRowProps {
        return {
            id: rowResponse.id,
            name: rowResponse.name,
            count: rowResponse.count,
            authorId: rowResponse.authorId
        }
    }

}
