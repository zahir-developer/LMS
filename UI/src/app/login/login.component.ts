import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { FormsModule } from '@angular/forms';

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
  constructor(private accService: AccountService) {

  }



  ngOnInit(): void {
    //alert('login-Init')
  }



  //using obervable with subscribe
  login() {
    this.accService.login(this.model).subscribe({
      next: result => {
        console.log(result);
        this.isLoggenIn = true;
        localStorage.setItem("IsloggedIn", "true");
        alert("Login successful !")
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
