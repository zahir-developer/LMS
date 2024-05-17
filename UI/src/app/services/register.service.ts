import { Injectable } from '@angular/core';
import { url } from '../config/config';
import { user } from '../model/user.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private httpClient: HttpClient) { }

  registerUser(userObj: user)
  {
    this.httpClient.post(url.dev + 'User/user', userObj).subscribe(
      result => {
        alert('User added successfully')
      }
    )
  }

}
