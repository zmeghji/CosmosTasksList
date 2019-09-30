import { Component } from '@angular/core';
import {TaskListService} from './taskList.service';
import { TaskList } from './taskList';
import {FormControl} from '@angular/forms';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(taskListService : TaskListService){
    this.date =  new FormControl(new Date())
    this._taskListService = taskListService;
    this._taskListService.getTaskList(new Date()).subscribe(
      taskList=>{
        this.taskList = taskList;
      }
    );
  }
  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this._taskListService.getTaskList(event.value).subscribe(
      taskList=>{
        this.taskList = taskList;
      }
    );
    console.log("change event caught");
  }
  private _taskListService : TaskListService;
  taskList:TaskList;
  date:FormControl;
  TaskListService
  title = 'tasklist';
}
