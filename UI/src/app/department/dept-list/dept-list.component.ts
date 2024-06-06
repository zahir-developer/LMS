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


@Component({
  selector: 'app-dept-list',
  standalone: true,
  imports: [CommonModule],
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
  }

  getDepartments() {
    this.deptService.getDepartments().subscribe({
      next: result => this.departments = result
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


}
