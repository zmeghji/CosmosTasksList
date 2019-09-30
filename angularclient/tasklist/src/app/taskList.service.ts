import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
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
    getTaskList(date: Date):Observable<TaskList>{
        return this._httpClient.get<TaskList>("https://localhost:44335/profile/1/tasklist/"+ date.toISOString());
    }
    createTaskList(date: Date):Observable<TaskList>{
        var taskList = new TaskList();
        taskList.profileId = 1;
        var now = new Date();
        taskList.date = new Date(now.getFullYear(),now.getMonth(),now.getDay())
        taskList.tasks = [];
        const httpOptions = {
            headers: new HttpHeaders({
              'Content-Type':  'application/json'
            })
          };
        return this._httpClient.post<TaskList>("https://localhost:44335/profile/1/tasklist/"+ date.toISOString()
        , taskList, httpOptions)
    }
   
}