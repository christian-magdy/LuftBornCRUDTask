import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { TaskListComponent } from './pages/task-list/task-list.component';
import { CreateTaskComponent } from './pages/create-task/create-task.component';
import { EditTaskComponent } from './pages/edit-task/edit-task.component';

export const appRoutes: Routes = [

    { path: '', redirectTo: 'login', pathMatch: 'full' }, 

  {
    path: 'login',
    component: LoginComponent
  },

  {
    path: '',
    loadComponent: () =>
      import('./layout/layout/layout.component').then(m => m.LayoutComponent),
    children: [
      { path: 'tasks', component: TaskListComponent },
      { path: 'create-task', component: CreateTaskComponent },
      { path: 'edit/:id',component:EditTaskComponent}
    ]
  },

  { path: '**', redirectTo: 'login' }
];