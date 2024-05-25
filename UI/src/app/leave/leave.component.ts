import { Component } from '@angular/core';
import { LeaveService } from '../services/leave.service';
import { UserLeave } from '../model/leave/user.leave';
import { CommonModule } from '@angular/common';
import { map } from 'rxjs';
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
import { MatDialogConfirmationComponent } from '../shared/mat-dialog-confirmation/mat-dialog-confirmation.component';
import { Title } from '@angular/platform-browser';
import { ConfirmDialogeResponse } from '../model/confirm.dialoge.response';
import { Confirm, LeaveStatus } from '../model/Enum/constEnum';
import { LeaveUpdate } from '../model/leave/leave.update';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-leave',
  standalone: true,
  imports: [CommonModule, RouterModule,
    MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent, MatDialogModule],
  templateUrl: './leave.component.html',
  styleUrl: './leave.component.css'
})
export class LeaveComponent {

  leaves: UserLeave[] = []
  leaveUpdate: LeaveUpdate = {
    id: 0,
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

  ) { }

  ngOnInit() {
    this.getAllLeave();
  }

  getAllLeave() {
    this.leaveService.getAllLeave().subscribe({
      next: result =>
        this.leaves = result
    });
    //   pipe(
    //   map((response: UserLeave[]) => {
    //     this.leaves = response;
    //   })
    // );
  }

  openApproveDialog(id: number) {
    this.dialogData.title = "Approve";
    this.dialogData.description = "Confirm leave approve action.!";
    this.openDialog(id)
  }

  openRejectDialog(id: number) {
    this.dialogData.title = "Reject";
    this.dialogData.description = "Confirm leave reject action.!";
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
        if (obj.type === LeaveStatus.Approved.toString())
          status = LeaveStatus.Approved;
        else
          status = LeaveStatus.Rejected;
      }

      this.leaveUpdate.id = obj.data;
      this.leaveUpdate.status = status;

      this.updateleaveStatus();

    });
  }

  updateleaveStatus() {
    this.leaveService.updateLeaveStatus(this.leaveUpdate).subscribe({
      next: result => {
        if (result)
          alert('Leave status updated successfully !');
      }
    })
  }
}
