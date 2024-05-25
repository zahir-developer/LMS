import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

  isLoggedIn = false;

  constructor(private accountService: AccountService, private route: Router)
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
    this.route.navigateByUrl('/login');
  }

  ngOnInit(): void
  {
    this.getCurrentUser();
  }

}
