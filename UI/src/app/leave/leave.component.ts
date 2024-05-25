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
  dialogData: any =
  {
    title: 'Confirm',
    description: 'Please confirm your action..!'
  }
  constructor(private leaveService: LeaveService,
    public dialog: MatDialog
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

  openApproveDialog()
  {
    this.dialogData.title = "Approve";
    this.dialogData.description = "Confirm your approve action.!";
    this.openDialog()
  }

  openRejectDialog()
  {
    this.dialogData.title = "Reject";
    this.dialogData.description = "Confirm your reject action.!";
    this.openDialog()
  }

  openDialog() {
    const dialog = this.dialog.open(MatDialogConfirmationComponent, {
      width: '250px',
      enterAnimationDuration: '0ms',
      exitAnimationDuration: "0ms",
      data: this.dialogData
    });
    
    var leaveStatus: any;
    dialog.afterClosed().subscribe((obj) => {
      leaveStatus = obj;
      console.log(obj);
    });
  }
}
