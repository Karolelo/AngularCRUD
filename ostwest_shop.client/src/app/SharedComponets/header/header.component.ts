import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon'
import { RouterModule } from '@angular/router';
import {UserService} from '../../services/UserService/user.service';
@Component({
  selector: 'app-header',
  standalone: false,

  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  constructor(private userService: UserService) {}

  isLoggedIn(): boolean {
    return this.userService.isLoggedIn();
  }

  isAdmin(): boolean {
    return this.userService.isAdmin();
  }

  logout(): void {
    this.userService.logout();
  }
}
