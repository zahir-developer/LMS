import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { AccountService } from './services/account.service';
import { LoginUser } from './model/login.user';
import { RegisterComponent } from './user/register/register.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RegisterComponent, NavComponent, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'LMS';

  constructor(private accountService: AccountService) {

  }

  ngOnInit(): void
  {
    this.setCurrentUser();
  }

  setCurrentUser()
  {
    const userString = localStorage.getItem('user');
    if(!userString) return;
    
    const user: LoginUser = JSON.parse(userString);

    this.accountService.setCurrentUser(user);

  }

}


