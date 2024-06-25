import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiEndPoint } from '../config/url.config';
import { LeaveTypeModel } from '../model/leave/leave.type.model';
import { environment } from '../../environments/environment';
import { BehaviorSubject } from 'rxjs';
import { PageListConfig, PagedListResult } from '../model/paged.list';

@Injectable({
  providedIn: 'root'
})
export class LeaveTypeService {

  private cancelEventSource = new BehaviorSubject<string>('list');

  cancelEdit$ = this.cancelEventSource.asObservable();

  constructor(private http: HttpClient) { }

  getLeaveTypeSearch(pageConfig: PageListConfig, searchText: string) {
    var query = environment.apiUrl + apiEndPoint.LeaveType.search;

    query = query.replace('{pgSize}', pageConfig.pageSize.toString())
      .replace('{pgNo}', pageConfig.pageNumber.toString())
      .replace('{sortBy}', pageConfig.sortBy.toString())
      .replace('{sortDir}', pageConfig.sortDir.toString())

    if (searchText != "" && searchText !== undefined)
      query = query.replace('{searchText}', searchText.toString());
    else
      query = query.replace('{searchText}', '').replace('&SearchText=', '');

    return this.http.get<PagedListResult<LeaveTypeModel>>(query);
  }

  getLeaveTypes() {
    return this.http.get<LeaveTypeModel[]>(environment.apiUrl + apiEndPoint.LeaveType.get);
  }

  addLeaveType(leaveType: LeaveTypeModel)
  {
    return this.http.post(environment.apiUrl + apiEndPoint.LeaveType.add, leaveType)
  }

  updateLeaveType(leaveType: LeaveTypeModel)
  {
    return this.http.put(environment.apiUrl + apiEndPoint.LeaveType.update, leaveType)
  }

  deleteLeaveType(leaveTypeId: number)
  {
    return this.http.delete(environment.apiUrl + apiEndPoint.LeaveType.delete.replace('{leaveTypeId}', leaveTypeId.toString()))
  }


  changeEvent(mode: string)
  {
    this.cancelEventSource.next(mode);
  }

}
