import { Injectable } from '@angular/core';
import { User } from '../model/user.model';
import { HttpUtilsService } from '../Util/http-utils.service';
import { apiEndPoint } from '../config/url.config';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { LoginUser } from '../model/login.user';
import { environment } from '../../environments/environment';
import { NotifyMessageService } from './notify-message.service';
import { AppText } from '../model/Enum/constEnum';
import { Roles } from '../model/Enum/constEnum';
import { PageListConfig } from '../model/paged.list';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  loggedInUserId: number = 0;
  loggedInUser: LoginUser | undefined;
  loggedInUserRole: string | undefined;
  loggedInUserDepartmentId: number = 0;
  isAdmin: boolean = false;
  isEmployee: boolean = false;
  isManager: boolean = false;

  private editUserSource = new BehaviorSubject<User | null>(null);
  editUser$ = this.editUserSource.asObservable();

  private currentUserSource = new BehaviorSubject<LoginUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(
    private httpUtilService: HttpUtilsService,
    private http: HttpClient,
    private _notify: NotifyMessageService,

  ) { }

  ngOnInit() {
    this.isAdmin = false;
    this.isManager = false;
    this.isEmployee = false;
  }

  registerUser(userObj: User) {
    this.httpUtilService.post(apiEndPoint.Auth.signup, userObj).subscribe(
      result => {
        this._notify.showMessage(AppText.UserCreatedSuccess);
      }
    );
  }

  getAllUser(pageConfig: PageListConfig, searchText: string) {
    var query = apiEndPoint.User.getAll.replace('{pgSize}', pageConfig.pageSize.toString())
      .replace('{pgNo}', pageConfig.pageNumber.toString())
      .replace('{sortBy}', pageConfig.sortBy.toString())
      .replace('{sortDir}', pageConfig.sortDir.toString())

    if (searchText != "" && searchText !== undefined)
      query = query.replace('{searchText}', searchText.toString());
    else if(searchText == "" || searchText == undefined)
      query = query.replace('{searchText}', '').replace('&SearchText=', '')
    else
      query = query;


    return this.httpUtilService.get(query);
  }

  checkEmailExists(emailId: string) {
    return this.http.get(environment.apiUrl + apiEndPoint.User.emailExists + emailId);
  }

  login_Old(model: any) {
    return this.http.post(environment.apiUrl + apiEndPoint.Auth.login.toString(), model);
  }

  updateUser(updateObj: User) {
    return this.http.put(environment.apiUrl + apiEndPoint.User.update, updateObj);
  }

  deleteUser(userId: number) {
    return this.http.delete(environment.apiUrl + apiEndPoint.User.delete.replace('{userId}', userId.toString()));
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


  setEditUser(editObj: User | undefined) {
    if (editObj)
      this.editUserSource.next(editObj);
  }

  setCurrentUser(user: LoginUser) {

    this.loggedInUser = user;
    if (user) {
      this.loggedInUserId = user?.id;
      this.loggedInUserRole = user?.role.roleName;
      this.loggedInUserDepartmentId = user.department?.id;
      if (user.role.roleName === Roles[Roles.Admin])
        this.isAdmin = true;
      else if (user.role.roleName == Roles[Roles.Manager])
        this.isManager = true;
      else if (user.role.roleName == Roles[Roles.Employee])
        this.isEmployee = true;
    }
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.isAdmin = false;
    this.isManager = false;
    this.isEmployee = false;
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

  getRoles() {
    return this.httpUtilService.get(apiEndPoint.Common.roles);
  }

}
