import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { User } from '../model/user.model';
import { last } from 'rxjs';
import { AccountService } from '../services/account.service';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  constructor(private accService: AccountService)
  {

  }

  public regForm: any;

  regData: User = {
    firstName: "",
    lastName: "",
    password: "",
    mobileNo: "",
    emailId: "",
    address:
    {
      country: "",
      state: ""
    }
  }

  ngOnInit() {
    this.regForm = new FormGroup(
      {
        firstName: new FormControl(this.regData.firstName, [Validators.required]),
        lastName: new FormControl(this.regData.lastName, Validators.required),
        email: new FormControl(this.regData.emailId, [Validators.required,Validators.email]),
        password: new FormControl(this.regData.password, Validators.required)
      }
    );
  }


  onSubmit() {
    this.accService.registerUser(this.regForm.value)
  }
}
