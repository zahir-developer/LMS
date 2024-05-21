import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

  isLoggedIn = false;

  constructor(private accountService: AccountService)
  {

  }

  
  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => this.isLoggedIn = !!user
    })
  }

  logout()
  {
    this.accountService.logout();
  }

  ngOnInit(): void
  {
    console.log('OnInit - Nav')
    this.getCurrentUser();

    // if(localStorage.getItem("IsloggedIn") === 'true')
    //     this.isLoggedIn = true;
    //   else
    //     this.isLoggedIn = false;
  }

}
