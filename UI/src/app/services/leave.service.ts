import { Injectable } from '@angular/core';
import { User } from '../model/user.model';
import { HttpUtilsService } from '../Util/http-utils.service';
import { apiEndPoint } from '../config/url.config';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { UserLeaveAdd } from '../model/leave/user.leave.add';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {
  constructor(private httpUtilService: HttpUtilsService, private http: HttpClient) { }

  addLeave(userLeaveObj: UserLeaveAdd) {
    this.httpUtilService.post(apiEndPoint.Leave.add, userLeaveObj).subscribe(
      result => {
        alert("Leave applied successfully");
      }
    );
  }

  getAllLeave() {
    return this.httpUtilService.get(apiEndPoint.Leave.getAll);
  }

  getLeave(userId: number) {
    this.httpUtilService.get(apiEndPoint.Leave.get).subscribe(
      result => {
        alert("Retrieved leaves successfully");
      }
    );
  }

  getLeaveType()
  {
    return this.httpUtilService.get(apiEndPoint.Leave.getType);
  }
  
}
