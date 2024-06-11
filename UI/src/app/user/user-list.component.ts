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
import { NotifyMessageService } from '../services/notify-message.service';

import { PaginationModule, PaginationConfig } from 'ngx-bootstrap/pagination';

import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { PagedListResult } from '../model/paged.list';
@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterModule,
    MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent, MatDialogModule,
    PaginationModule,
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent {
  showBoundaryLinks: boolean = true;
  showDirectionLinks: boolean = true;
  contentArray: string[] = new Array(50).fill('');
  returnedArray: string[] = [];
  pgNo: number = 1;
  pgSize: number = 3;

  pagedList: PagedListResult<User> = {
    currentPage: 1,
    itemsPerPage: 3,
    totalItems: 0,
    totalPages: 0,
    items: [],
  }
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
    public dialog: MatDialog,
    private notify: NotifyMessageService
  ) { }

  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.pagedList.currentPage = event.page;
    this.getUsers();
  }

  ngOnInit() {
    this.getUsers();
    this.accountService.setEditUser(undefined);
    this.pagedList.items = this.allUsers;
  }
  getUsers() {
    this.accountService.getAllUser(this.pagedList.itemsPerPage, this.pagedList.currentPage).subscribe({
      next: result => {
        this.pagedList = result;
        console.log(this.pagedList.itemsPerPage);
        console.log(this.pagedList.totalItems);
       }
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
          if (result) {
            this.getUsers();
            this.notify.showMessage(AppText.DeleteSuccess);
          }
        }
      }
    )
  }


}
