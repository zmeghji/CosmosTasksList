import { Component } from '@angular/core';
import {TaskListService} from './taskList.service';
import { TaskList, Task } from './taskList';
import {FormControl} from '@angular/forms';
import {MatDatepickerInputEvent} from '@angular/material/datepicker';
import {MatIconRegistry} from '@angular/material/icon'
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
    this.newTaskName = new FormControl("");
    this._taskListService = taskListService;
    this._setTaskList(new Date());
  }
  addEvent(type: string, event: MatDatepickerInputEvent<Date>) {
    this._setTaskList(event.value);
  }
  private _updateTaskListInDb(): void{
    this._taskListService.updateTaskList(this.taskList).subscribe(
      taskList=>{console.log("updated db")}
    );
  }
  addTask() : void{
    var newTask = new Task();
    newTask.name = this.newTaskName.value;
    this.taskList.tasks.push(newTask)
    this._updateTaskListInDb()
  }
  removeTask(index:number){
    this.taskList.tasks.splice(index,1);
    this._updateTaskListInDb()
  }
  private _taskListService : TaskListService;
  taskList:TaskList;
  date:FormControl;
  newTaskName:FormControl;
  TaskListService
  title = 'tasklist';
}
