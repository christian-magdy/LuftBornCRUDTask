import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs'
import { Task } from '../models/task.model'

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  private api = 'http://localhost:5186/api/tasks'

  constructor(private http: HttpClient) {}

  getAll(): Observable<Task[]> {
    return this.http.get<Task[]>(this.api)
  }

  getById(id:number): Observable<Task> {
    return this.http.get<Task>(`${this.api}/${id}`)
  }

  create(task:Task): Observable<Task> {
    return this.http.post<Task>(this.api, task)
  }

  update(task:Task){
    return this.http.put(`${this.api}/${task.id}`, task)
  }

  delete(id:number){
    return this.http.delete(`${this.api}/${id}`)
  }
    updateTask(id: number, task: any){
    return this.http.put(`${this.api}/${id}`, task);
  }

}