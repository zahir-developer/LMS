import { ResultModel } from './../../model/common/result.model';
import { ToastrService } from 'ngx-toastr';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LeaveService } from '../../services/leave.service';
import { CommonModule, DatePipe, JsonPipe } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

import { MatNativeDateModule, provideNativeDateAdapter } from '@angular/material/core';
import { AccountService } from '../../services/account.service';
import { UserLeaveAdd } from '../../model/leave/user.leave.add';
import { Router } from '@angular/router';
import { LeaveReport } from '../../model/leave/leave.report';
import { LeaveTypeModel } from '../../model/leave/leave.type.model';
import { AppText } from '../../model/Enum/constEnum';
@Component({
  selector: 'app-leave-request',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule,
    MatFormFieldModule, MatInputModule, MatDatepickerModule, JsonPipe,
    MatNativeDateModule
  ],
  providers: [provideNativeDateAdapter(),],
  templateUrl: './leave-request.component.html',
  styleUrl: './leave-request.component.css'
})
export class LeaveRequestComponent {
  userId: number = 0;
  model: UserLeaveAdd = {
    userId: 0,
    departmentId: 0,
    leaveTypeId: 0,
    fromDate: new Date(),
    toDate: new Date(),
    comments: ''
  };
  leaveType: LeaveTypeModel[] = [];
  leaveRemaining: LeaveReport[] = [];
  remaingLeaveCount: number = 0;

  leaveBalance: LeaveReport = {
    name: '',
    leaveType: '',
    leaveTypeId: 0,
    totalLeave: 0,
    totalLeaveRemaining: 0,
    totalLeaveTaken: 0,
    userId: 0
  };
  constructor(
    private leaveService: LeaveService,
    private accountService: AccountService,
    private router: Router,
    private notify: ToastrService,
    private datePipe: DatePipe
  ) { }

  //leaveForm: FormGroup | undefined;
  leaveForm: FormGroup = new FormGroup({});
  initializeForm() {
    this.leaveForm = new FormGroup({
      userId: new FormControl(this.userId),
      leaveTypeId: new FormControl(0, [Validators.required, Validators.min(1)]),
      fromDate: new FormControl<Date | null>(this.model?.fromDate, [Validators.required]),
      toDate: new FormControl<Date | null>(this.model?.toDate, [Validators.required]),
      dateRange: new FormControl(),
      comments: new FormControl(this.model?.comments, [Validators.required]),
      departmentId: new FormControl(this.model?.departmentId)
    })
  }

  ngOnInit() {
    this.getLeaveType();
    this.getCurrentUser();
    this.initializeForm();
    this.getLeaveAvailability();
  }

  onSubmit() {
    if (this.leaveForm.valid) {
      this.addLeave(this.leaveForm.value);
    }
  }

  addLeave(objData: UserLeaveAdd) {

    var days = 0;
    var fromDay = Number(this.datePipe.transform(objData.fromDate, 'dd'));
    var toDay = Number(this.datePipe.transform(objData.toDate, 'dd'));

    if (+toDay > +fromDay)
      days = toDay - fromDay;
    else
      days = fromDay - toDay;

    days = days + 1;

    //if (this.remaingLeaveCount >= days) {
    this.leaveService.addLeave(objData).subscribe(res => {

      if (res.result) {
        console.log('Leave applied successfully');
        this.router.navigateByUrl('/user-leave');
      }
      else
        this.notify.warning(AppText.LeaveCanNotBeAppliedOnHoliday, 'Apply leave restricted');
    })
    //} else {
    //  this.notify.warning('Not enough leaves remaining: ' + this.remaingLeaveCount, 'Apply leave restricted');
    //}

  }

  leaveTypeChanged() {
    this.remaingLeaveCount = this.checkLeaveAvailability();
  }

  checkLeaveAvailability() {
    var result = 0;

    var leaveBalance = this.leaveRemaining.filter(s => s.leaveTypeId == this.leaveForm.value.leaveTypeId);

    if (leaveBalance)
      result = leaveBalance[0].totalLeaveRemaining;

    return result;
  }

  getLeaveAvailability() {
    var result = false;
    this.leaveService.getUserLeaveReport(this.userId).subscribe(
      {
        next: response => {
          if (response) {
            this.leaveRemaining = response;
          }
        }
      })
  }
  getLeaveType() {
    this.leaveService.getLeaveType().subscribe(
      result => {
        if (result) {
          this.leaveType = result.filter(q => q.isEnabled == true);
        }

      }
    )
  }

  getCurrentUser() {
    this.userId = this.accountService.getUserId();
    this.model.departmentId = this.accountService.loggedInUserDepartmentId;
  }

}
