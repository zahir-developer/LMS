import { Injectable } from '@angular/core';
import { User } from '../model/user.model';
import { HttpUtilsService } from '../Util/http-utils.service';
import { apiEndPoint } from '../config/url.config';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private httpUtilService: HttpUtilsService) { }

  registerUser(userObj: User) {
    this.httpUtilService.post(apiEndPoint.User.add, userObj).subscribe(
      result => {
        alert("Operation completed successfully");
      }
    );
  }
}
