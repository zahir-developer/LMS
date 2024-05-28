import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router, RouterModule } from '@angular/router';
import { Role } from '../model/Enum/constEnum';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

  isLoggedIn = false;
  isAdmin: boolean | undefined;
  loggedInUser: string | undefined;
  loggedInUserRole: string | undefined;
  constructor(private accountService: AccountService, private route: Router) {

  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: user => {
        this.isLoggedIn = !!user
        if (user) {
          this.loggedInUser = user?.firstName;
          this.loggedInUserRole = user.role.roleName;
          if (user.role.roleName === Role[Role.Admin])
            this.isAdmin = true;
          else
            this.isAdmin = false;
        }
      }
    })
  }



  logout() {
    this.accountService.logout();
    this.route.navigateByUrl('/login');
  }

  ngOnInit(): void {
    this.getCurrentUser();
  }
}
