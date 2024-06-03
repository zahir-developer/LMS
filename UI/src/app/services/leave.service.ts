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

  getLeave(userId: number) {
    return this.httpUtilService.get(apiEndPoint.Leave.get.replace('{userId}', userId.toString()));
  }

  getAllLeave(departmentId: number) {
    return this.httpUtilService.get(apiEndPoint.Leave.getAll.replace('{departmentId}', departmentId.toString()));
  }

  getLeaveType() {
    return this.httpUtilService.get(apiEndPoint.Leave.getType);
  }

  updateLeaveStatus(obj: LeaveUpdate) {
    return this.httpUtilService.put(apiEndPoint.Leave.updateStatus, obj);
  }

  getLeaveReport(userId: number) {
    return this.httpUtilService.get(apiEndPoint.Leave.report.replace('{userId}', userId.toString()))
  }

}
