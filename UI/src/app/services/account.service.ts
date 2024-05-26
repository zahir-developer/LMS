import { Injectable } from '@angular/core';
import { User } from '../model/user.model';
import { HttpUtilsService } from '../Util/http-utils.service';
import { apiEndPoint } from '../config/url.config';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { LoginUser, Role } from '../model/login.user';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  loggedInUserId: number = 0;
  loggedInUser: LoginUser | undefined;
  loggedInUserRole: string | undefined;

  private currentUserSource = new BehaviorSubject<LoginUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private httpUtilService: HttpUtilsService, private http: HttpClient) { }

  registerUser(userObj: User) {
    this.httpUtilService.post(apiEndPoint.Auth.signup, userObj).subscribe(
      result => {
        alert("Operation completed successfully");
      }
    );
  }

  getAllUser() {
    this.httpUtilService.get(apiEndPoint.User.getAll).subscribe(
      result => {
      }
    );
  }

  login_Old(model: any) {
    return this.http.post(environment.apiUrl + apiEndPoint.Auth.login.toString(), model);
  }

  login(model: any) {
    return this.http.post<LoginUser>(environment.apiUrl + apiEndPoint.Auth.login, model).pipe(
      map((response: LoginUser) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
        }
      }
      )
    )
  }

  setCurrentUser(user: LoginUser) {
    this.currentUserSource.next(user);
    this.loggedInUser = user;
    if (user) {
      this.loggedInUserId = user?.id;
      this.loggedInUserRole = user?.role.roleName;
    }

  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.loggedInUserId = 0;
  }

  getToken() {
    const userTokenString = localStorage.getItem('token')?.toString();

    if (!userTokenString) return "";

    const user = JSON.parse(userTokenString);

    return user.token;
  }

  getUserId() {
    return this.loggedInUserId;
  }

  getCurrentUser() {
    this.currentUser$.subscribe({
      next: user => {
        if (user)
          this.loggedInUser = user;
      }
    });
    return this.loggedInUser;
  }

  getCurrentUserRole() {
    return this.loggedInUserRole;
  }
}
