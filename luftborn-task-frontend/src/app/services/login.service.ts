import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private api = 'http://localhost:5186/api/Auth'

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {

    return this.http.post<any>(`${this.api}/login`, {
      username: username,
      password: password
    });

  }

}