import { Component,inject } from '@angular/core';
import {UserService} from '../../services/UserService/user.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login-page',
  standalone: false,

  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
  password!: string;
  username!: string;
  router = inject(Router);
  errorMessage: string | null = null;
  constructor(private userService: UserService) { }
  onSubmit() {
    const credentials = {
      username: this.username,
      password: this.password
    }
    this.userService.login(credentials).subscribe({
      next: (data) => {
        console.log('Poprawnie zalogowano',data);
        this.router.navigate(['productList']);
      },
      error: (error) => {
        this.errorMessage = 'Nieprawidłowe dane logowania. Spróbuj ponownie.';
        console.error('Błąd logowania:', error);
      }
    })
  }
}
