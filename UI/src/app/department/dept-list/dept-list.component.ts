import { Department } from './../../model/login.user';
import { DepartmentModel } from './../../model/department/department.model';
import { Component, EventEmitter, Output } from '@angular/core';
import { DepartmentService } from '../../services/department.service';
import { CommonModule } from '@angular/common';
import { emitDistinctChangesOnlyDefaultValue } from '@angular/compiler';


@Component({
  selector: 'app-dept-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dept-list.component.html',
  styleUrl: './dept-list.component.css'
})
export class DeptListComponent {
  @Output() editEvent = new EventEmitter<DepartmentModel>();
  constructor(
    private deptService: DepartmentService
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
  edit(dept: DepartmentModel) {
    this.editEvent.emit(dept);
    this.deptService.changeEvent('edit')
  }

  onAdd()
  {
    this.editEvent.emit(this.department);
    this.deptService.changeEvent('add')
  }

  openDeleteDialog(deptId: number) {

  }

}
