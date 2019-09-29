export class TaskList{
    profileId: number;
    date: Date;
    tasks: Task[];
}
export class Task{
    name: string;
    status: number;
}