export class TaskList{
    profileId: number;
    date: string;
    tasks: Task[];
}
export class Task{
    name: string;
    status: number;
}