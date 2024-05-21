import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
   { path: 'home', component: HomeComponent },
   { path: 'login', component: LoginComponent },
   { path: 'register', component: RegisterComponent }   
];

