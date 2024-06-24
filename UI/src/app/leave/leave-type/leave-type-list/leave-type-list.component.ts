import { ConfirmDialogeResponse } from '../../../model/confirm.dialoge.response';
import { LeaveTypeModel } from '../../../model/leave/leave.type.model';
import { Component, EventEmitter, Output } from '@angular/core';
import { LeaveTypeService } from '../../../services/leave-type.service';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogConfirmationComponent } from '../../../shared/mat-dialog-confirmation/mat-dialog-confirmation.component';
import { ConfirmDialog, MatDialogHelper } from '../../../model/common/mat.dialog.helper';
import { AppText, Confirm } from '../../../model/Enum/constEnum';
import { ToastrService } from 'ngx-toastr';
import { PagedListResult } from '../../../model/paged.list';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { MatSortModule, Sort } from '@angular/material/sort';

@Component({
  selector: 'app-leave-type-list',
  standalone: true,
  imports: [CommonModule,
            PaginationModule,
            FormsModule,
            MatSortModule
          ],
  templateUrl: './leave-type-list.component.html'
})
export class LeaveTypeListComponent {
  @Output() editEvent = new EventEmitter<LeaveTypeModel>();
  matDialogRef: any;
  dialogData: ConfirmDialog =
    {
      title: 'Delete leave type',
      description: AppText.DeleteConfirmation.toString(),
      data: 0
    }
  showBoundaryLinks: boolean = true;
  showDirectionLinks: boolean = true;
  pagedList: PagedListResult<LeaveTypeModel> = {
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
  constructor(
    private leaveTypeService: LeaveTypeService,
    private dialog: MatDialog,
    private matDialogHelper: MatDialogHelper,
    private toastrService: ToastrService
  ) { }

  leaveType: LeaveTypeModel = {
    id: 0,
    leaveTypeName: '',
    description: '',
    maxLeaveCount: 0,
    isEnabled: true
  }
  leaveTypes: LeaveTypeModel[] = []

  ngOnInit() {
    this.getLeaveTypes();
    this.pagedList.items = this.leaveTypes;
  }

  getLeaveTypes() {
    this.leaveTypeService.getLeaveTypeSearch(this.pagedList.pageListConfig, this.pagedList.searchText).subscribe({
      next: result => this.pagedList = result
    })
  }

  onAdd() {
    this.editEvent.emit(this.leaveType);
    this.leaveTypeService.changeEvent('add')
  }

  edit(leaveType: LeaveTypeModel) {
    this.editEvent.emit(leaveType);
    this.leaveTypeService.changeEvent('edit')
  }

  deleteLeaveType(leaveTypeId: number) {
    this.leaveTypeService.deleteLeaveType(leaveTypeId).subscribe({
      next: result => {
        if (result) {
          this.toastrService.success(AppText.DeleteSuccess, 'LeaveType Delete');
          this.getLeaveTypes();
        }
      }
    })
  }

  openDeleteDialog(leaveTypeId: number) {
    this.dialogData.data = leaveTypeId;
    var config = this.matDialogHelper.getMatDialogConfig(this.dialogData)
    this.matDialogRef = this.dialog.open(MatDialogConfirmationComponent, config);
    this.deleteDialogClose();
  }

  deleteDialogClose() {
    this.matDialogRef.afterClosed().subscribe(
      (obj: ConfirmDialogeResponse) => {
        if (obj.action == Confirm.Yes)
          this.deleteLeaveType(obj.data);
      }
    )
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
      this.getLeaveTypes();
    }
  }

  pageChanged(event: PageChangedEvent): void {
    // const startItem = (event.page - 1) * event.itemsPerPage;
    // const endItem = event.page * event.itemsPerPage;
    this.pagedList.pageListConfig.pageNumber = event.page;
    this.getLeaveTypes();
  }

  onKeydown(key: string) {
    if (key === "Enter") {
      this.getLeaveTypes();
    }
  }


}
