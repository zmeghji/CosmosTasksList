import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {TaskList} from "./taskList";
@Injectable({
    providedIn: 'root'
})
export class TaskListService{
    private _httpClient : HttpClient
    constructor(httpClient: HttpClient){
        this._httpClient = httpClient;
    }
    getTaskList():Observable<TaskList>{
        // var taskList = new TaskList();
        // taskList.ProfileId = 1;
        // return taskList;
        return this._httpClient.get<TaskList>("https://localhost:44335/profile/1/tasklist/2019-09-25T00:00:00");
    }
    // getTaskList():Observable<TaskList>{
        
    // }
}