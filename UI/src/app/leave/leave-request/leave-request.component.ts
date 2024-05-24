import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LeaveService } from '../../services/leave.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-leave-request',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './leave-request.component.html',
  styleUrl: './leave-request.component.css'
})
export class LeaveRequestComponent {
  leaveType: any = [];
  constructor(private leaveService: LeaveService){}

  //leaveForm: FormGroup | undefined;
  leaveForm: FormGroup = new FormGroup({});
  initializeForm() {
    this.leaveForm = new FormGroup({
      leaveType: new FormControl(),
      fromDate: new FormControl(),
      toDate: new FormControl(),
      comments: new FormControl()
    })
  }

  ngOnInit()
  {
    this.getLeaveType();
    this.initializeForm();
  }

  onSubmit()
  {

  }

  getLeaveType()
  {
    this.leaveService.getLeaveType().subscribe(
      result => {
        this.leaveType = result;
      }
    )
  }

}
