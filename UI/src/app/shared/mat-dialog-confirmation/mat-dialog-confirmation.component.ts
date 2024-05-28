import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogTitle,
  MatDialogContent,
  MatDialogRef
} from '@angular/material/dialog';
import { ConfirmDialogeResponse } from '../../model/confirm.dialoge.response';

@Component({
  selector: 'app-mat-dialog-confirmation',
  standalone: true,
  imports: [MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent, MatButtonModule],
  templateUrl: './mat-dialog-confirmation.component.html',
  styleUrl: './mat-dialog-confirmation.component.css'
})
export class MatDialogConfirmationComponent {
  dialogHeading: string = "Leave "
  message: string = "Please Approve or Reject leave..."
  cancelButtonText = "Cancel"

  constructor(public dialogRef: MatDialogRef<MatDialogConfirmationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { title: string, description: string, data: any }
  ) {

  }
  onConfirm(type: string, action: any): void {
    const response: ConfirmDialogeResponse =
    {
      type: type,
      action: action,
      data: this.data.data
    }
    this.dialogRef.close(response);
  }
}


