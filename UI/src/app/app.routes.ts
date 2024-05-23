import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { authGuard } from './guards/auth.guard';
import { LeaveComponent } from './leave/leave.component';

export const routes: Routes = [
   { path: 'home', component: HomeComponent, canActivate: [authGuard] },
   { path: 'login', component: LoginComponent },
   { path: 'register', component: RegisterComponent },
   { path: 'leave', component: LeaveComponent }
];

