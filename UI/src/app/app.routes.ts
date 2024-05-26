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

export const routes: Routes = [
   { path: 'home', component: HomeComponent, canActivate: [authGuard] },
   { path: 'login', component: LoginComponent },
   { path: 'register', component: RegisterComponent },
   { path: 'leave', component: LeaveComponent },
   { path: 'leave-request', component: LeaveRequestComponent },
   { path: 'user-leave', component: UserLeaveComponent },
   { path: 'user-list', component: UserListComponent },
   { path: 'user-edit', component: UserEditComponent }
];

