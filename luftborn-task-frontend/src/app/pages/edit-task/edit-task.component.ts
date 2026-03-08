import { Component,OnInit } from '@angular/core'
import { ActivatedRoute,Router } from '@angular/router'
import { TaskService } from '../../services/task.service'
import { CommonModule } from '@angular/common'
import { FormsModule } from '@angular/forms';

@Component({
 selector:'app-edit-task',
 standalone:true,
 imports: [CommonModule, FormsModule],
 templateUrl:'./edit-task.component.html'
})
export class EditTaskComponent implements OnInit{

 task:any={}

 constructor(
  private route:ActivatedRoute,
  private service:TaskService,
  private router:Router
 ){}

 ngOnInit(){

  const id=this.route.snapshot.params['id']

  this.service.getById(id)
  .subscribe(res=>{
    this.task=res
  })

 }

 update(){

  this.service.update(this.task)
  .subscribe(()=>{
    this.router.navigate(['/tasks'])
  })

 }

}