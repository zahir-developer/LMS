import { ConfirmDialogeResponse } from './../../model/confirm.dialoge.response';
import { Department } from './../../model/login.user';
import { DepartmentModel } from './../../model/department/department.model';
import { Component, EventEmitter, Output } from '@angular/core';
import { DepartmentService } from '../../services/department.service';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogConfirmationComponent } from '../../shared/mat-dialog-confirmation/mat-dialog-confirmation.component';
import { ConfirmDialog, MatDialogHelper } from '../../model/common/mat.dialog.helper';
import { AppText, Confirm } from '../../model/Enum/constEnum';
import { ToastrService } from 'ngx-toastr';
import { PagedListResult } from '../../model/paged.list';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { PaginationModule, PaginationConfig } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-dept-list',
  standalone: true,
  imports: [CommonModule,
    PaginationModule,
    FormsModule],
  templateUrl: './dept-list.component.html',
  styleUrl: './dept-list.component.css'
})
export class DeptListComponent {
  @Output() editEvent = new EventEmitter<DepartmentModel>();
  matDialogRef: any;
  dialogData: ConfirmDialog =
    {
      title: 'Delete department',
      description: AppText.DeleteConfirmation.toString(),
      data: 0
    }
  showBoundaryLinks: boolean = true;
  showDirectionLinks: boolean = true;
  pagedList: PagedListResult<DepartmentModel> = {
    pageListConfig: {
      pageNumber: 1,
      pageSize: 3,
      totalItems: 0,
      totalPages: 0,
    },
    searchText: '',
    items: [],
  }
  constructor(
    private deptService: DepartmentService,
    private dialog: MatDialog,
    private matDialogHelper: MatDialogHelper,
    private toastrService: ToastrService
  ) { }

  department: DepartmentModel = {
    id: 0,
    departmentName: '',
    description: ''
  }
  departments: DepartmentModel[] = []

  ngOnInit() {
    this.getDepartments();
    this.pagedList.items = this.departments;
  }

  getDepartments() {
    this.deptService.getDepartmentSearch(this.pagedList.pageListConfig, this.pagedList.searchText).subscribe({
      next: result => this.pagedList = result
    })
  }

  onAdd() {
    this.editEvent.emit(this.department);
    this.deptService.changeEvent('add')
  }

  edit(dept: DepartmentModel) {
    this.editEvent.emit(dept);
    this.deptService.changeEvent('edit')
  }

  deleteDepartment(deptId: number) {
    this.deptService.deleteDepartment(deptId).subscribe({
      next: result => {
        if (result) {
          this.toastrService.success(AppText.DeleteSuccess, 'Department Delete');
          this.getDepartments();
        }
      }
    })
  }

  openDeleteDialog(deptId: number) {
    this.dialogData.data = deptId;
    var config = this.matDialogHelper.getMatDialogConfig(this.dialogData)
    this.matDialogRef = this.dialog.open(MatDialogConfirmationComponent, config);
    this.deleteDialogClose();
  }

  deleteDialogClose() {
    this.matDialogRef.afterClosed().subscribe(
      (obj: ConfirmDialogeResponse) => {
        if (obj.action == Confirm.Yes)
          this.deleteDepartment(obj.data);
      }
    )
  }

  pageChanged(event: PageChangedEvent): void {
    // const startItem = (event.page - 1) * event.itemsPerPage;
    // const endItem = event.page * event.itemsPerPage;
    this.pagedList.pageListConfig.pageNumber = event.page;
    this.getDepartments();
  }

  onKeydown(key: string) {
    if (key === "Enter") {
      this.getDepartments();
    }
  }
}
