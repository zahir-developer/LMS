export interface LeaveReport {
  userId: number;
  name: string;
  leaveTypeId: number;
  leaveType: string;
  totalLeave: number;
  totalLeaveTaken: number;
  totalLeaveRemaining: number;
}
