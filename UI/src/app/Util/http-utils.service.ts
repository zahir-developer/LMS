

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { apiBaseUrl } from '../config/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpUtilsService {

  constructor(private http: HttpClient) { }

  public post(url: string, data?: any, optional?: any): Observable<any> {
    return this.http.post(apiBaseUrl.url + url, data, optional);
  }
  public get(url: string, optional?: any): Observable<any> {
    return this.http.get( apiBaseUrl.url + url, optional);
  }
  public put(url: string, data?: any): Observable<any> {
    return this.http.put(apiBaseUrl.url + url, data);
  }
  public delete(url: string, optional?: any): Observable<any> {
    return this.http.delete(apiBaseUrl.url  + url, optional);
  }
}
