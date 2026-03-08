import { Component } from '@angular/core'
import { TaskService } from '../../services/task.service'
import { Router } from '@angular/router'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector:'app-create-task',
  standalone:true,
  imports: [CommonModule, FormsModule],

  templateUrl:'./create-task.component.html'
})
export class CreateTaskComponent{

  task:any={}

  constructor(
    private service:TaskService,
    private router:Router
  ){}

  save(){
    this.service.create(this.task)
    .subscribe(()=>{
      this.router.navigate(['/tasks'])
    })
  }

}