import { Component } from '@angular/core';
import { AccountService } from '../services/account.service';
import { AppText, Confirm } from '../model/Enum/constEnum';
import { CommonModule } from '@angular/common';
import {
  MatDialog,
  MatDialogRef,
  MatDialogActions,
  MatDialogClose,
  MatDialogTitle,
  MatDialogContent,
  MatDialogModule,
} from '@angular/material/dialog';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogConfirmationComponent } from '../shared/mat-dialog-confirmation/mat-dialog-confirmation.component';
import { ConfirmDialogeResponse } from '../model/confirm.dialoge.response';
import { User } from '../model/user.model';


@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterModule,
    MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent, MatDialogModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent {
  allUsers: User[] = [{
    id: 0,
    firstName: '',
    lastName: '',
    roleName: '',
    roleId: 0,
    password: '',
    mobileNo: '',
    email: '',
    address:
    {
      country: '',
      state: ''
    }
  }];
  dialogData: any =
    {
      title: 'Delete confirmation',
      description: AppText.DeleteConfirmation.toString(),
      data: 0
    }
  constructor(private accountService: AccountService,
    private router: Router,
    public dialog: MatDialog) { }

  ngOnInit() {
    this.getUsers();
    this.accountService.setEditUser(undefined);
  }
  getUsers() {
    this.accountService.getAllUser().subscribe({
      next: result => this.allUsers = result
    });
  }
  edit(editUser: User) {
    this.accountService.setEditUser(editUser);
    this.router.navigateByUrl('/user-edit');
  }

  openDeleteDialog(id: number) {
    this.openDialog(id)
  }

  openDialog(id: number) {
    this.dialogData.data = id;
    const dialog = this.dialog.open(MatDialogConfirmationComponent, {
      width: '250px',
      enterAnimationDuration: '0ms',
      exitAnimationDuration: '0ms',
      data: this.dialogData
    });

    dialog.afterClosed().subscribe((obj: ConfirmDialogeResponse) => {
      console.log(obj);
      var status: number = 0;
      if (obj.action === Confirm.Yes.toString()) {
        this.deleteUser(obj.data);
      }
    });
  }

  deleteUser(id: number) {
    this.accountService.deleteUser(id).subscribe(
      {
        next: result => {
          if(result)
            {
              this.getUsers();
              alert(AppText.DeleteSuccess);
            } 
        }
      }
    )
  }


}
