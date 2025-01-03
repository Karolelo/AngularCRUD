import { Component, OnInit } from '@angular/core';
import {UserService} from './services/UserService/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit  {

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.logout()
    console.log('Local storage został wyczyszczony na starcie aplikacji');
  }
}
