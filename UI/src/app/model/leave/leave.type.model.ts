export interface LeaveTypeModel {
  id: number;
  leaveTypeName: string;
  maxLeaveCount: number;
  description: string | null;
  isEnabled: boolean | null;
}
