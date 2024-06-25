import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './guards/auth.guard';
import { LeaveComponent } from './leave/leave/leave.component';
import { LeaveRequestComponent } from './leave/leave-request/leave-request.component';
import { UserLeaveComponent } from './leave/leave/user-leave/user-leave.component';
import { UserListComponent } from './user/user-list.component';
import { RegisterComponent } from './user/register/register.component';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { LeaveReportComponent } from './leave/leave-report/leave-report.component';
import { DeptListComponent } from './department/dept-list/dept-list.component';
import { DeptAddEditComponent } from './department/dept-add-edit/dept-add-edit.component';
import { DepartmentComponent } from './department/department.component';
import { LeaveTypeComponent } from './leave/leave-type/leave-type.component';


export const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [authGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent, canActivate: [authGuard] },
  { path: 'leave', component: LeaveComponent, canActivate: [authGuard] },
  { path: 'leave-request', component: LeaveRequestComponent, canActivate: [authGuard] },
  { path: 'leave-report', component: LeaveReportComponent, canActivate: [authGuard] },
  { path: 'user-leave', component: UserLeaveComponent, canActivate: [authGuard] },
  { path: 'user-list', component: UserListComponent, canActivate: [authGuard] },
  { path: 'user-edit', component: UserEditComponent, canActivate: [authGuard] },
  //Department
  { path: 'dept', component: DepartmentComponent, canActivate: [authGuard] },
  { path: 'dept?action=list', component: DepartmentComponent, canActivate: [authGuard] },
  { path: 'dept?action=edit', component: DepartmentComponent, canActivate: [authGuard] },
  { path: 'dept?action=add', component: DepartmentComponent, canActivate: [authGuard] },

  { path: 'leave-type', component: LeaveTypeComponent, canActivate: [authGuard] },
  //{ path: 'dept-edit', loadChildren: () => import('./department/dept-edit/dept-edit.component'), canActivate: [authGuard] }
];

