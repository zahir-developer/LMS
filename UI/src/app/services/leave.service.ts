import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../Util/http-utils.service';
import { apiEndPoint } from '../config/url.config';
import { HttpClient } from '@angular/common/http';
import { UserLeaveAdd } from '../model/leave/user.leave.add';
import { LeaveUpdate } from '../model/leave/leave.update';
import { LeaveTypeModel } from '../model/leave/leave.type.model';
import { environment } from '../../environments/environment';

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
    return this.http.get<LeaveTypeModel[]>(environment.apiUrl + apiEndPoint.Leave.getType);
  }

  updateLeaveStatus(obj: LeaveUpdate) {
    return this.httpUtilService.put(apiEndPoint.Leave.updateStatus, obj);
  }

  getLeaveReport(departmentId: number) {
    return this.httpUtilService.get(apiEndPoint.Leave.report.replace('{departmentId}', departmentId.toString()), );
  }

  getUserLeaveReport(userId: number) {
    return this.httpUtilService.get(apiEndPoint.Leave.userReport.replace('{userId}', userId.toString()))
  }

}
