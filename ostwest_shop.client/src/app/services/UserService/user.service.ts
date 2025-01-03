import { Injectable,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {User} from '../../Intefraces/user';
import {Credentials} from '../../Intefraces/credentials';
import { tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class UserService{

  private baseUrl = 'https://localhost:7200/';
  constructor(private http: HttpClient) { }

  register(user: any) :Observable<User>
  {
    return this.http.post<User>(`${this.baseUrl}Accounts`,user);
  }

  login(credentials: any): Observable<Credentials> {
    return this.http.post<Credentials>(`${this.baseUrl}Accounts/login`, credentials).pipe(
      tap((response: any) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('role', response.role);
      })
    );
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {
    return localStorage.getItem('role');
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  isAdmin(): boolean {
    return this.getRole() === 'admin';
  }
}
