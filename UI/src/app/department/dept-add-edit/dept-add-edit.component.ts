import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DepartmentModel } from '../../model/department/department.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { DepartmentService } from '../../services/department.service';

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
    private deptService: DepartmentService
  ) { }
  public formDept: any;

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
      departmentName: new FormControl(this.deptModel.departmentName, [Validators.required]),
      description: new FormControl(this.deptModel.description)
    })
  }

  onSubmit()
  {
    if(this.formDept.valid)
      {

      }
  }


}
