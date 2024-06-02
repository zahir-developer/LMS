import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router, RouterModule } from '@angular/router';
import { Roles } from '../model/Enum/constEnum';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

  isLoggedIn = false;
  isAdmin: boolean = false;
  isManager: boolean = false;
  isEmployee: boolean = false;
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
          if (user.role.roleName === Roles[Roles.Admin])
            this.isAdmin = true;
          else if(user.role.roleName == Roles[Roles.Manager])
            this.isManager = true;
          else if(user.role.roleName == Roles[Roles.Employee])
            this.isEmployee = true;
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
