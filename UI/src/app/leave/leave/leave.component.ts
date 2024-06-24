import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
@Component({
  selector: 'app-user-leave',
  standalone: true,
  imports: [],
  templateUrl: './leave.component.html',
  styleUrl: './leave.component.css'
})
export class LeaveComponent {
  currentUserRole: boolean = false;
  constructor(private accountService: AccountService) {

  }
}
