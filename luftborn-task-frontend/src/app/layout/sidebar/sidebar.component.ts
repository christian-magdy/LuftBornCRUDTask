import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Router } from '@angular/router'

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './sidebar.component.html'
})
export class SidebarComponent {  
  constructor(
      private router:Router
    ){}
  logout() {
    localStorage.removeItem('token');   // remove JWT
    this.router.navigate(['/login']);   // go to login
  }}