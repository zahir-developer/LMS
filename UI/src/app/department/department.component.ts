import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DeptListComponent } from './dept-list/dept-list.component';

@Component({
  selector: 'app-department',
  standalone: true,
  imports: [CommonModule, DeptListComponent],
  templateUrl: './department.component.html',
  styleUrl: './department.component.css'
})
export class DepartmentComponent {


}
