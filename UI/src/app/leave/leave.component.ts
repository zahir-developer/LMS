import { Component } from '@angular/core';
import { LeaveService } from '../services/leave.service';
import { UserLeave } from '../model/leave/user.leave';
import { CommonModule } from '@angular/common';
import { map } from 'rxjs';

@Component({
  selector: 'app-leave',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './leave.component.html',
  styleUrl: './leave.component.css'
})
export class LeaveComponent {

  leaves: UserLeave[] = []
  constructor(private leaveService: LeaveService) { }

  ngOnInit() {
    console.log('ngOnInit');
    this.getAllLeave();
  }

  getAllLeave()
  {
    this.leaveService.getAllLeave().subscribe({
      next: result => 
        this.leaves = result
    });
    //   pipe(
    //   map((response: UserLeave[]) => {
    //     this.leaves = response;
    //   })
    // );
  }
}
