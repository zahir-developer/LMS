import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LeaveTypeAddEditComponent } from './leave-type-add-edit/leave-type-add-edit.component';
import { LeaveTypeListComponent } from './leave-type-list/leave-type-list.component';
import { LeaveTypeModel } from '../../model/leave/leave.type.model';
import { LeaveTypeService } from '../../services/leave-type.service';

@Component({
  selector: 'app-leave-type',
  standalone: true,
  imports: [
    CommonModule,
    LeaveTypeListComponent,
    LeaveTypeAddEditComponent,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './leave-type.component.html'
})

export class LeaveTypeComponent {
  action: string = "";
  modelLeaveType: LeaveTypeModel = {
    id: 0,
    leaveTypeName: '',
    description: '',
    maxLeaveCount: 0,
    isEnabled: true
  };

  constructor(
    private router: Router,
    private leaveTypeService: LeaveTypeService,
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
    this.leaveTypeService.cancelEdit$.subscribe(
      {
        next: result => this.action = result
      }
    )
  }

  onEdit(leaveType: any) {
    this.modelLeaveType = leaveType;
    this.action = 'edit';
  }

  onEditCancelEvent() {
    this.action = 'list';
  }

  onAddEvent() {
    this.action = 'add';

  }
}
