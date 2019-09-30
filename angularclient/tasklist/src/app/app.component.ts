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
  private _setTaskList(date: Date){
    this.taskList = null;
    this._taskListService.getTaskList(date).subscribe(
      taskList=>{
        this.taskList = taskList;
      },
      error => {
        if (error.status ==404){
          this._taskListService.createTaskList(date).subscribe(
            taskList => {this.taskList = taskList;}
          );
        // console.log(error);
        }
      }
    );
  }
  constructor(taskListService : TaskListService){
    this.date =  new FormControl(new Date())
    this._taskListService = taskListService;
    this._setTaskList(new Date());
  }
  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this._setTaskList(event.value);
  }
  private _taskListService : TaskListService;
  taskList:TaskList;
  date:FormControl;
  TaskListService
  title = 'tasklist';
}
