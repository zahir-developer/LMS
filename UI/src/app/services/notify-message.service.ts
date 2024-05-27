import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotifyMessageService {

  constructor(
    private _notify: MatSnackBar
  ) { }

  showMessage(message: string) {
    this._notify.open(message, 'Close');
  }
}
