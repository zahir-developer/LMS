import { Injectable } from '@angular/core';
import { User } from '../model/user.model';
import { HttpUtilsService } from '../Util/http-utils.service';
import { apiEndPoint } from '../config/url.config';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { UserLeaveAdd } from '../model/leave/user.leave.add';
import { LeaveUpdate } from '../model/leave/leave.update';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {
  constructor(private httpUtilService: HttpUtilsService, private http: HttpClient) { }

  addLeave(userLeaveObj: UserLeaveAdd) {
    return this.httpUtilService.post(apiEndPoint.Leave.add, userLeaveObj);
  }

  getAllLeave() {
    return this.httpUtilService.get(apiEndPoint.Leave.getAll);
  }

  getLeave(userId: number) {
    return this.httpUtilService.get(apiEndPoint.Leave.get + userId);
  }

  getLeaveType() {
    return this.httpUtilService.get(apiEndPoint.Leave.getType);
  }

  updateLeaveStatus(obj: LeaveUpdate) {
    return this.httpUtilService.put(apiEndPoint.Leave.updateStatus, obj);
  }

}
