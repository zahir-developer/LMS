import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiEndPoint } from '../config/url.config';
import { DepartmentModel } from '../model/department/department.model';
import { environment } from '../../environments/environment';
import { BehaviorSubject } from 'rxjs';
import { PageListConfig, PagedListResult } from '../model/paged.list';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  modelDept = {
    id: 0,
    departmentName: '',
    description: ''
  }

  private cancelEventSource = new BehaviorSubject<string>('list');

  cancelEdit$ = this.cancelEventSource.asObservable();

  constructor(private http: HttpClient) { }

  getDepartmentSearch(pageConfig: PageListConfig, searchText: string) {
    var query = environment.apiUrl + apiEndPoint.Department.search;

    query = query.replace('{pgSize}', pageConfig.pageSize.toString())
      .replace('{pgNo}', pageConfig.pageNumber.toString())

    if (searchText != "" && searchText !== undefined)
      query = query.replace('{searchText}', searchText.toString());
    else
      query = query.replace('{searchText}', '').replace('&SearchText=', '');

    return this.http.get<PagedListResult<DepartmentModel>>(query);
  }

  getDepartments() {
    return this.http.get<DepartmentModel[]>(environment.apiUrl + apiEndPoint.Department.get);
  }

  addDepartment(dept: DepartmentModel)
  {
    return this.http.post(environment.apiUrl + apiEndPoint.Department.add, dept)
  }

  updateDepartment(dept: DepartmentModel)
  {
    return this.http.put(environment.apiUrl + apiEndPoint.Department.update, dept)
  }

  deleteDepartment(deptId: number)
  {
    return this.http.delete(environment.apiUrl + apiEndPoint.Department.delete.replace('{departmentId}', deptId.toString()))
  }


  changeEvent(mode: string)
  {
    this.cancelEventSource.next(mode);
  }

}
