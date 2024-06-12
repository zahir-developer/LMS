import { Component, Input, input } from '@angular/core';
import { LeaveService } from '../../services/leave.service';
import { UserLeave } from '../../model/leave/user.leave';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import {
  MatDialog,
  MatDialogRef,
  MatDialogActions,
  MatDialogClose,
  MatDialogTitle,
  MatDialogContent,
  MatDialogModule,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogConfirmationComponent } from '../../shared/mat-dialog-confirmation/mat-dialog-confirmation.component';
import { ConfirmDialogeResponse } from '../../model/confirm.dialoge.response';
import { AppText, Confirm, LeaveStatus, LeaveStatusText } from '../../model/Enum/constEnum';
import { LeaveUpdate } from '../../model/leave/leave.update';
import { AccountService } from '../../services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-leave',
  standalone: true,
  imports: [CommonModule, RouterModule,
    MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent, MatDialogModule],
  templateUrl: './user-leave.component.html',
  styleUrl: './user-leave.component.css'
})
export class UserLeaveComponent {
  isEmployee: boolean = false;
  isManager: boolean = false;
  leaves: UserLeave[] = []
  userDepartmentId: number = 0;
  leaveStatusText: typeof LeaveStatusText = LeaveStatusText;
  leaveUpdate: LeaveUpdate = {
    Id: 0,
    status: LeaveStatus.Pending,
    userId: this.accountService.getUserId()
  };

  dialogData: any =
    {
      title: 'Confirm',
      description: 'Please confirm your action..!',
      data: 0
    }
  constructor(private leaveService: LeaveService,
    private accountService: AccountService,
    public dialog: MatDialog,
    private toastr: ToastrService,
  ) { }

  ngOnInit() {
    this.getLeave();
    this.isManager = this.accountService.isManager;
    this.isEmployee = this.accountService.isEmployee;
  }

  getLeave() {
    const userId = this.accountService.loggedInUserId;
    const departmentId = this.accountService.loggedInUserDepartmentId;
    if (this.accountService.isManager) {
      this.leaveService.getAllLeave(departmentId).subscribe({
        next: result =>
          this.leaves = result
      });
    }
    else if (this.accountService.isEmployee) {
      this.leaveService.getLeave(userId).subscribe({
        next: result =>
          this.leaves = result
      });
    }

  }

  openApproveDialog(id: number) {
    this.dialogData.title = LeaveStatusText.Approved.toString();
    this.dialogData.description = AppText.ApproveConfirmation.toString();
    this.openDialog(id);
  }

  openRejectDialog(id: number) {
    this.dialogData.title = LeaveStatusText.Rejected.toString();
    this.dialogData.description = AppText.RejectConfirmation.toString();
    this.openDialog(id)
  }

  openDialog(id: number) {
    this.dialogData.data = id;
    const dialog = this.dialog.open(MatDialogConfirmationComponent, {
      width: '250px',
      enterAnimationDuration: '0ms',
      exitAnimationDuration: "0ms",
      data: this.dialogData
    });

    dialog.afterClosed().subscribe((obj: ConfirmDialogeResponse) => {
      console.log(obj);
      var status: number = 0;

      if (obj.action === Confirm.Yes.toString()) {
        if (obj.type === LeaveStatus[LeaveStatus.Approved])
          status = LeaveStatus.Approved;
        else
          status = LeaveStatus.Rejected;

        this.leaveUpdate.Id = obj.data;
        this.leaveUpdate.status = status;

        this.updateleaveStatus();
      }
    });
  }

  updateleaveStatus() {
    if (this.isManager) {
      this.leaveService.updateLeaveStatus(this.leaveUpdate).subscribe({
        next: result => {
          if (result) {
            this.toastr.success('Leave status updated successfully !', 'User Leave');
            this.getLeave();
          }
        }
      })
    }
    else
      this.toastr.warning(AppText.ForbiddenAction, 'Restricted');

  }
}
