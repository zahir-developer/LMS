import { Component, Input, OnInit, } from '@angular/core';
import { DepartmentModel } from '../../model/department/department.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { DepartmentService } from '../../services/department.service';
import { ToastrService } from 'ngx-toastr';
import { AppModule, AppText } from '../../model/Enum/constEnum';


@Component({
  selector: 'app-dept-add-edit',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ],
  templateUrl: './dept-add-edit.component.html',
  styleUrl: './dept-add-edit.component.css'
})
export class DeptAddEditComponent implements OnInit {


  constructor(
    private fb: FormBuilder,
    private router: Router,
    private deptService: DepartmentService,
    private toastrService: ToastrService
  ) { }
  public formDept: any;
  isSubmitted: boolean = false;

  @Input() deptModel: DepartmentModel = {
    id: 0,
    departmentName: '',
    description: ''
  }

  isAdd: boolean = true;
  ngOnInit(): void {

    if (this.deptModel?.id == 0)
      this.isAdd = true;
    else
      this.isAdd = false;

    this.InitializeForm();

  }

  onBack() {
    this.deptService.changeEvent('list');
  }

  InitializeForm() {
    this.formDept = this.fb.group({
      id: new FormControl(this.deptModel.id),
      departmentName: new FormControl(this.deptModel.departmentName, [Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
      description: new FormControl(this.deptModel.description, Validators.maxLength(50))
    })
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.formDept.valid) {

      if (this.isAdd)
        this.addDepartment();
      else
        this.updateDepartment();

    }
  }

  addDepartment() {
    this.deptService.addDepartment(this.formDept.value).subscribe(
      {
        next: result => {
          if (result)
            this.toastrService.success(AppText.AddDepartmentSuccess, AppModule.Department);
        }
      }
    )
  }

  updateDepartment() {
    this.deptService.updateDepartment(this.formDept.value).subscribe(
      {
        next: result => {
          if (result)
            this.toastrService.success(AppText.UpdateDepartmentSuccess, AppModule.Department);
        }
      }
    )
  }

  deleteDepartment(departmentId: number) {
    this.deptService.deleteDepartment(departmentId).subscribe(
      {
        next: result => {
          if (result)
            this.toastrService.success(AppText.DeleteDepartmentSuccess, AppModule.Department);
        }
      }
    )
  }
}
