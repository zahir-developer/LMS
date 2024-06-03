import { Component, EventEmitter, Input, OnInit, Output, ɵprovideZonelessChangeDetection } from '@angular/core';
import { FormControl, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { User } from '../../model/user.model';
import { AccountService } from '../../services/account.service';
import { CommonModule } from '@angular/common';
import { AppText } from '../../model/Enum/constEnum';
import { NotifyMessageService } from '../../services/notify-message.service';
import { DepartmentModel } from '../../model/department/department.model';
import { DepartmentService } from '../../services/department.service';
@Component({
  selector: 'app-user-edit',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './user-edit.component.html',
  styleUrl: './user-edit.component.css'
})
export class UserEditComponent {
  submitted = false;
  constructor(
    private accService: AccountService,
    private notify: NotifyMessageService,
    private department: DepartmentService,
  ) {
  }
  roles: any;
  departments: DepartmentModel[] = [];
  public formEditUser: any;
  editUserData: User = {
    id: 0,
    firstName: '',
    lastName: '',
    roleId: 0,
    roleName: '',
    departmentId: 0,
    departmentName: '',
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
    this.getDepartments();
    this.accService.editUser$.subscribe({
      next: editUser => {
        if (editUser) {
          console.log(editUser);
          this.editUserData = editUser;
          this.initializeForm();
        }
      }
    })
  }

  initializeForm() {
    if (this.editUserData.id !== 0) {
      this.formEditUser = new FormGroup(
        {
          firstName: new FormControl(this.editUserData.firstName, [Validators.required]),
          lastName: new FormControl(this.editUserData.lastName, Validators.required),
          roleId: new FormControl(this.editUserData.roleId, [Validators.required]),
          departmentId: new FormControl(this.editUserData.departmentId, [Validators.required]),
          email: new FormControl(this.editUserData.email, [Validators.required, Validators.email]),
        }
      );
    }
  }


  onSubmit() {
    this.submitted = true;
    console.log(this.formEditUser.value);
    if (this.formEditUser.valid) {
      const updateUser : User = this.formEditUser.value;
      updateUser.id = this.editUserData.id;
      this.accService.updateUser(updateUser).subscribe(
        {
          next: result => {
            if (result) this.notify.showMessage(AppText.UserUpdateSuccess)
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

  getDepartments() {
    this.department.getDepartments().subscribe(
      {
        next: result => {
          if(result)
            this.departments = result;
        }
      }
    )
  }
}
