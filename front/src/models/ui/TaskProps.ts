export interface TaskProps {
    id: number;
    name: string;
    shortDescription: string;
    detailedDescription: string;
    solution: string;
    tableName: string;
    authorId: number;
    done: boolean;
}
