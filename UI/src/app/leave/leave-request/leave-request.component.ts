import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LeaveService } from '../../services/leave.service';
import { CommonModule, JsonPipe } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

import { provideNativeDateAdapter } from '@angular/material/core';
import { AccountService } from '../../services/account.service';
import { UserLeaveAdd } from '../../model/leave/user.leave.add';
import { Router } from '@angular/router';
import { LoginUser } from '../../model/login.user';
@Component({
  selector: 'app-leave-request',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule,
    MatFormFieldModule, MatInputModule, MatDatepickerModule, JsonPipe,
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './leave-request.component.html',
  styleUrl: './leave-request.component.css'
})
export class LeaveRequestComponent {
  userId: number | undefined;
  model: UserLeaveAdd = {
    userId: 0,
    leaveTypeId: 0,
    fromDate: new Date(),
    toDate: new Date(),
    comments: ''
  };
  leaveType: any = [];
  constructor(
    private leaveService: LeaveService,
    private accountService: AccountService,
    private router: Router) { }

  //leaveForm: FormGroup | undefined;
  leaveForm: FormGroup = new FormGroup({});
  initializeForm() {
    this.leaveForm = new FormGroup({
      leaveType: new FormControl("", [Validators.required]),
      fromDate: new FormControl<Date | null>(this.model?.fromDate, [Validators.required]),
      toDate: new FormControl<Date | null>(this.model?.toDate, [Validators.required]),
      dateRange: new FormControl(),
      comments: new FormControl(this.model?.comments, [Validators.required])
    })
  }

  ngOnInit() {
    this.getLeaveType();
    this.initializeForm();
    this.getCurrentUser();
  }

  onSubmit() {
    console.log(this.model);
    console.log(this.leaveForm.value);
    if (this.leaveForm.valid) {
      this.model.userId = this.userId;
      this.model.comments = this.leaveForm.value.comments;
      this.model.leaveTypeId = this.leaveForm.value.leaveType;
      this.addLeave(this.model);
    }

  }

  addLeave(objData: UserLeaveAdd) {
    this.leaveService.addLeave(objData).subscribe(
      result => {
        console.log('Leave applied successfully');
        this.router.navigateByUrl('/leave');
      }
    )
  }
  getLeaveType() {
    this.leaveService.getLeaveType().subscribe(
      result => {
        this.leaveType = result;
      }
    )
  }

  getCurrentUser() {
    this.userId = this.accountService.getUserId();
  }

}
