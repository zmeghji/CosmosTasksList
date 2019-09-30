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
    getTaskList(date: Date):Observable<TaskList>{
        return this._httpClient.get<TaskList>("https://localhost:44335/profile/1/tasklist/"+ date.toISOString());
    }
}