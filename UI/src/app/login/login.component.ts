import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  model: any = {}
  isLoggenIn = false;
  constructor(private accService: AccountService, private router: Router) {

  }



  ngOnInit(): void {
    //alert('login-Init')
  }



  //using obervable with subscribe
  login() {
    this.accService.login(this.model).subscribe({
      next: result => {
        this.isLoggenIn = true;
        this.router.navigateByUrl('/home')

      },
      error: error => console.log(error)
    })
  }

  loginPipe(model: any) {
    //this.accService.login(this.)
  }

  logout() {
    this.accService.logout();
    localStorage.setItem("IsloggedIn", "false");
  }
}
