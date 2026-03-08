import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NgIf } from '@angular/common';
import { SidebarComponent } from './layout/sidebar/sidebar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgIf, SidebarComponent],
  templateUrl: './app.component.html'
})
export class AppComponent {

  constructor(private router: Router) {}

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    const isLoginPage = this.router.url === '/login';

    return !!token && !isLoginPage;
  }

}