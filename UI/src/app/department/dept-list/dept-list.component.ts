import { Component } from '@angular/core';
import { DepartmentService } from '../../services/department.service';
import { DepartmentModel } from '../../model/department/department.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dept-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dept-list.component.html',
  styleUrl: './dept-list.component.css'
})
export class DeptListComponent {
  constructor(
    private deptService: DepartmentService
  ) { }

  departments: DepartmentModel[] = []

  ngOnInit() {
    this.getDepartments();
  }

  getDepartments() {
    this.deptService.getDepartments().subscribe({
      next: result => this.departments = result
    })
  }


}
