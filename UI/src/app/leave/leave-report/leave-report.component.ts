import { AccountService } from './../../services/account.service';
import { Component } from '@angular/core';
import { LeaveService } from '../../services/leave.service';
import { LeaveReport } from '../../model/leave/leave.report';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-leave-report',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './leave-report.component.html',
  styleUrl: './leave-report.component.css'
})
export class LeaveReportComponent {
  userId: number = 0
  departmentId: number = 0;
  isEmployee: boolean = false;
  isManager: boolean = false;
  leaveReport: LeaveReport[] = [];
  constructor(
    private accountService: AccountService,
    private leaveService: LeaveService
  ) { }

  ngOnInit() {
    this.initUserDetail();
    this.getReport();
  }

  initUserDetail() {
    this.userId = this.accountService.loggedInUserId;
    this.isEmployee = this.accountService.isEmployee;
    this.isManager = this.accountService.isManager;
    this.departmentId = this.accountService.loggedInUserDepartmentId;
  }

  getReport() {
    if (this.isEmployee)
      this.getUserLeaveReport();
    else if (this.isManager)
      this.getLeaveReport();
    else {
      alert('Restricted access !!!')
    }
  }

  getUserLeaveReport() {
    this.leaveService.getUserLeaveReport(this.userId).subscribe({
      next: result => this.leaveReport = result
    })
  }
  getLeaveReport() {
    this.leaveService.getLeaveReport(this.departmentId).subscribe({
      next: result => this.leaveReport = result
    })
  }

}
