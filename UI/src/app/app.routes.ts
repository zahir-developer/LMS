import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './guards/auth.guard';
import { LeaveComponent } from './leave/leave.component';
import { LeaveRequestComponent } from './leave/leave-request/leave-request.component';
import { UserLeaveComponent } from './leave/user-leave/user-leave.component';
import { UserListComponent } from './user/user-list.component';
import { RegisterComponent } from './user/register/register.component';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { LeaveReportComponent } from './leave/leave-report/leave-report.component';

export const routes: Routes = [
   { path: 'home', component: HomeComponent, canActivate: [authGuard] },
   { path: 'login', component: LoginComponent },
   { path: 'register', component: RegisterComponent, canActivate: [authGuard] },
   { path: 'leave', component: LeaveComponent, canActivate: [authGuard] },
   { path: 'leave-request', component: LeaveRequestComponent, canActivate: [authGuard] },
   { path: 'leave-report', component: LeaveReportComponent, canActivate: [authGuard] },
   { path: 'user-leave', component: UserLeaveComponent, canActivate: [authGuard] },
   { path: 'user-list', component: UserListComponent, canActivate: [authGuard] },
   { path: 'user-edit', component: UserEditComponent, canActivate: [authGuard] }
];

