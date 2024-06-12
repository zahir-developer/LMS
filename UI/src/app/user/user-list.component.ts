import { PageListConfig } from './../model/paged.list';
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
import { ConfirmDialog } from '../model/common/mat.dialog.helper';
import { MatDialogHelper } from '../model/common/mat.dialog.helper';

import { PaginationModule, PaginationConfig } from 'ngx-bootstrap/pagination';

import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { PagedListResult } from '../model/paged.list';
import { FormsModule } from '@angular/forms';

import { Sort, MatSortModule } from '@angular/material/sort';
@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule, RouterModule,
    MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent, MatDialogModule,
    PaginationModule,
    FormsModule,
    MatSortModule,
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent {
  showBoundaryLinks: boolean = true;
  showDirectionLinks: boolean = true;

  pagedList: PagedListResult<User> = {
    pageListConfig: {
      pageNumber: 1,
      pageSize: 3,
      totalItems: 0,
      totalPages: 0,
      sortBy: 'Id',
      sortDir: 'asc'
    },
    searchText: '',
    items: [],
  }
  allUsers: User[] = [{
    id: 0,
    firstName: '',
    lastName: '',
    roleName: '',
    roleId: 0,
    departmentId: 0,
    departmentName: '',
    password: '',
    mobileNo: '',
    email: '',
    address:
    {
      country: '',
      state: ''
    }
  }];
  dialogData: ConfirmDialog =
    {
      title: 'Delete user',
      description: AppText.DeleteConfirmation.toString(),
      data: 0
    }
  constructor(private accountService: AccountService,
    private router: Router,
    public dialog: MatDialog,
    private notify: NotifyMessageService,
    private matDialogHelper: MatDialogHelper,
  ) { }

  pageChanged(event: PageChangedEvent): void {
    const startItem = (event.page - 1) * event.itemsPerPage;
    const endItem = event.page * event.itemsPerPage;
    this.pagedList.pageListConfig.pageNumber = event.page;
    this.getUsers();
  }

  ngOnInit() {
    this.getUsers();
    this.accountService.setEditUser(undefined);
    this.pagedList.items = this.allUsers;
  }
  getUsers() {
    this.accountService.getAllUser(this.pagedList.pageListConfig, this.pagedList.searchText).subscribe({
      next: result => {
        this.pagedList = result;
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
    var config = this.matDialogHelper.getMatDialogConfig(this.dialogData);
    const dialog = this.dialog.open(MatDialogConfirmationComponent, config);

    dialog.afterClosed().subscribe((obj: ConfirmDialogeResponse) => {
      if (obj.action === Confirm.Yes) {
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

  onKeydown(key: string) {
    if (key === "Enter") {
      this.getUsers();
    }
  }

  sortData(sort: Sort) {
    const data = this.pagedList.items.slice();
    if (!sort.active || sort.direction === '') {
      this.pagedList.items = data;
      return;
    }
    const isAsc = sort.direction === 'asc';

    if (sort.active != '') {
      this.pagedList.pageListConfig.sortBy = sort.active;
      this.pagedList.pageListConfig.sortDir = sort.direction;
      this.getUsers();
    }
  }
}
