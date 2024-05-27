import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { User } from '../../model/user.model';
import { AccountService } from '../../services/account.service';
import { CommonModule } from '@angular/common';
import { NotifyMessageService } from '../../services/notify-message.service';
import { AppText } from '../../model/Enum/constEnum';
import { Router } from '@angular/router';
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  roles: any;
  submitted = false;
  isEmailValid: any;
  constructor(
    private accService: AccountService,
    private _notify: NotifyMessageService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {

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
    this.regForm = this.formBuilder.group(
      {
        firstName: new FormControl(this.regData.firstName, [Validators.required]),
        lastName: new FormControl(this.regData.lastName, Validators.required),
        roleId: new FormControl(this.regData.roleId, [Validators.required]),
        email: new FormControl(this.regData.email, [Validators.required, Validators.email]),
        password: new FormControl(this.regData.password, Validators.required)
      }
    );
    console.log(this.regForm);
    console.log(this.regForm.controls.email.touched);
  }

  onSubmit() {
    this.submitted = true;
    console.log(this.regForm.value)
    if (this.regForm.valid) {
      this.checkEmailExists();
    }
  }
  checkEmailExists() {

    var emailId = this.regForm.value.email;
    if (emailId) {
      this.accService.checkEmailExists(emailId).subscribe({
        next: result => {
          this.isEmailValid = (result);
          if (result)
            this._notify.showMessage(AppText.EmailExists);
          else {
            this.accService.registerUser(this.regForm.value);
            //this._notify.showMessage(AppText.EmailNotExists);
            this.router.navigateByUrl('/user-list');
          }

        }
      })
    }
  }
  getRoles() {
    this.accService.getRoles().subscribe({
      next: result =>
        this.roles = result
    });
  }

}
