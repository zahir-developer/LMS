

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpUtilsService {
  apiBaseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  public post(url: string, data?: any, optional?: any): Observable<any> {
    return this.http.post(this.apiBaseUrl + url, data, optional);
  }
  public get(url: string, optional?: any): Observable<any> {
    return this.http.get(this.apiBaseUrl + url, optional);
  }
  public put(url: string, data?: any): Observable<any> {
    return this.http.put(this.apiBaseUrl + url, data);
  }
  public delete(url: string, optional?: any): Observable<any> {
    return this.http.delete(this.apiBaseUrl  + url, optional);
  }
}
