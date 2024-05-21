import { Injectable } from '@angular/core';
import { User } from '../model/user.model';
import { HttpUtilsService } from '../Util/http-utils.service';
import { apiEndPoint } from '../config/url.config';
import { HttpClient } from '@angular/common/http';
import { apiBaseUrl } from '../config/environment';
import { BehaviorSubject, map } from 'rxjs';
import { LoginUser } from '../model/login.user';
 
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new BehaviorSubject<LoginUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  
  constructor(private httpUtilService: HttpUtilsService, private http: HttpClient) { }

  registerUser(userObj: User) {
    this.httpUtilService.post(apiEndPoint.User.add, userObj).subscribe(
      result => {
        alert("Operation completed successfully");
      }
    );
  }

  login_Old(model: any)
  {
    return this.http.post(apiBaseUrl.url + apiEndPoint.User.login.toString(), model);
  }

  login(model: any)
  {
    return this.http.post<LoginUser>(apiBaseUrl.url + apiEndPoint.User.login, model).pipe(
      map((response: LoginUser) =>
        {
          const user = response;
          if(user)
            {
              localStorage.setItem('user', JSON.stringify(user));
              this.currentUserSource.next(user);
            }
        }
      )
    )
  }

  setCurrentUser(user: LoginUser)
  {
    this.currentUserSource.next(user);
  }

  logout()
  {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
