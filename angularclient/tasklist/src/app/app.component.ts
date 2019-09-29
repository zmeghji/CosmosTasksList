import { Component } from '@angular/core';
import {TaskListService} from './taskList.service';
import { TaskList } from './taskList';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(taskListService : TaskListService){
    this._taskListService = taskListService;
    this._taskListService.getTaskList().subscribe(
      taskList=>{
        console.log(taskList);
        this.taskList = taskList;
      }

    );
  }
  private _taskListService : TaskListService;
  taskList:TaskList;
  TaskListService
  title = 'tasklist';
}
