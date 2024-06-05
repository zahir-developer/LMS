import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiEndPoint } from '../config/url.config';
import { DepartmentModel } from '../model/department/department.model';
import { environment } from '../../environments/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  private cancelEventSource = new BehaviorSubject<string>('list');

  cancelEdit$ = this.cancelEventSource.asObservable();

  constructor(private http: HttpClient) { }

  getDepartments() {
    return this.http.get<DepartmentModel[]>(environment.apiUrl + apiEndPoint.Department.get);
  }

  changeEvent(mode: string)
  {
    this.cancelEventSource.next(mode);
  }

}
