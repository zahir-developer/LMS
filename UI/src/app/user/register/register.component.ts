import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { User } from '../../model/user.model';
import { AccountService } from '../../services/account.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  roles: any;
  constructor(private accService: AccountService) {

  }

  public regForm: any;

  regData: User = {
    id: 0,
    firstName: '',
    lastName: '',
    roleId: 0,
    roleName: '',
    password: '',
    mobileNo: '',
    email: '',
    address:
    {
      country: '',
      state: ''
    }
  }

  ngOnInit() {
    this.getRoles();
    this.regForm = new FormGroup(
      {
        firstName: new FormControl(this.regData.firstName, [Validators.required]),
        lastName: new FormControl(this.regData.lastName, Validators.required),
        roleId: new FormControl(this.regData.roleId, [Validators.required]),
        email: new FormControl(this.regData.email, [Validators.required, Validators.email]),
        password: new FormControl(this.regData.password, Validators.required)
      }
    );
  }


  onSubmit() {
    console.log(this.regForm.value)
    if (this.regForm.valid)
      this.accService.registerUser(this.regForm.value)
  }
  getRoles() {
    this.accService.getRoles().subscribe({
      next: result =>
        this.roles = result
    });
  }
}
