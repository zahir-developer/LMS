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
  leaveReport: LeaveReport[] = [];
  constructor(
    private accountService: AccountService,
    private leaveService: LeaveService
  ) { }

  ngOnInit() {
    this.userId = this.accountService.loggedInUserId;
    this.getLeaveReport();
  }

  getLeaveReport()
  {
    this.leaveService.getLeaveReport(this.userId).subscribe({
      next: result => this.leaveReport = result
    })
  }

}
