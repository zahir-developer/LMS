import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  loggedInUser: string = "";
  allUser: any = [];
  constructor(private accountService: AccountService)
  {

  }

  ngOnInit() {
    this.getCurrentUser();
    this.allUser = this.accountService.getAllUser();
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => this.loggedInUser = user?.email.toString()!
    })
  }
}
