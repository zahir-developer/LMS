import { Component, Input, OnInit, } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AppModule, AppText } from '../../../model/Enum/constEnum';
import { LeaveTypeService } from '../../../services/leave-type.service';
import { LeaveTypeModel } from '../../../model/leave/leave.type.model';


@Component({
  selector: 'app-leave-type-add-edit',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule
  ],
  templateUrl: './leave-type-add-edit.component.html',
  styleUrl: './leave-type-add-edit.component.css'
})
export class LeaveTypeAddEditComponent implements OnInit {


  constructor(
    private fb: FormBuilder,
    private router: Router,
    private leaveTypeService: LeaveTypeService,
    private toastrService: ToastrService
  ) { }
  public formLeaveType: any;
  isSubmitted: boolean = false;

  @Input() leaveTypeModel: LeaveTypeModel = {
    id: 0,
    leaveTypeName: '',
    description: '',
    maxLeaveCount: 0,
    isEnabled: true
  }

  isAdd: boolean = true;
  ngOnInit(): void {

    if (this.leaveTypeModel?.id == 0)
      this.isAdd = true;
    else
      this.isAdd = false;

    this.InitializeForm();

  }

  onBack() {
    this.leaveTypeService.changeEvent('list');
  }

  InitializeForm() {
    this.formLeaveType = this.fb.group({
      id: new FormControl(this.leaveTypeModel.id),
      leaveTypeName: new FormControl(this.leaveTypeModel.leaveTypeName, [Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
      maxLeaveCount: new FormControl(this.leaveTypeModel.maxLeaveCount, [Validators.required, Validators.min(1), Validators.max(90)]),
      description: new FormControl(this.leaveTypeModel.description, Validators.maxLength(50))
    })
  }

  onSubmit() {
    console.log(this.formLeaveType);
    this.isSubmitted = true;
    if (this.formLeaveType.valid) {

      if (this.isAdd)
        this.addLeaveType();
      else
        this.updateLeaveType();

    }
  }

  addLeaveType() {
    this.leaveTypeService.addLeaveType(this.formLeaveType.value).subscribe(
      {
        next: result => {
          if (result)
            this.toastrService.success(AppText.AddLeaveTypeSuccess, AppModule.LeaveType);
            this.onBack();
        }
      }
    )
  }

  updateLeaveType() {
    this.leaveTypeService.updateLeaveType(this.formLeaveType.value).subscribe(
      {
        next: result => {
          if (result)
            this.toastrService.success(AppText.UpdateLeaveTypeSuccess, AppModule.LeaveType);
            this.onBack();
        }
      }
    )
  }

  deleteLeaveType(leaveTypeId: number) {
    this.leaveTypeService.deleteLeaveType(leaveTypeId).subscribe(
      {
        next: result => {
          if (result)
            this.toastrService.success(AppText.DeleteLeaveTypeSuccess, AppModule.LeaveType);
        }
      }
    )
  }
}
