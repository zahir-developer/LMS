import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DeptListComponent } from './dept-list/dept-list.component';
import { Router } from '@angular/router';
import { DeptAddEditComponent } from './dept-add-edit/dept-add-edit.component';
import { DepartmentModel } from '../model/department/department.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DepartmentService } from '../services/department.service';

@Component({
  selector: 'app-department',
  standalone: true,
  imports: [
    CommonModule,
    DeptListComponent,
    DeptAddEditComponent,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})

export class DepartmentComponent {
  action: string = "";
  modelDept: DepartmentModel = {
    id: 0,
    departmentName: '',
    description: ''
  };

  constructor(
    private router: Router,
    private deptService: DepartmentService,
  ) {
    var index = this.router.url.indexOf('add');
    if (index > 0)
      this.action = 'add';
    else if (this.router.url.indexOf('edit') > 0)
      this.action = 'edit';
    else
      this.action = 'list';
  }

  ngOnInit() {
    this.deptService.cancelEdit$.subscribe(
      {
        next: result => this.action = result
      }
    )
  }

  onEdit(dept: any) {
    console.log('Edit event');
    console.log(dept);
    this.modelDept = dept;
    this.action = 'edit';
  }

  onEditCancelEvent() {
    this.action = 'list';
  }

  onAddEvent() {
    this.action = 'add';

  }
}
