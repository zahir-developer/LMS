export interface LeaveReport {
  userId: number;
  name: string;
  leaveType: string;
  totalLeave: number;
  totalLeaveTaken: number;
  totalLeaveRemaining: number;
}
