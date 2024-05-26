import { LeaveStatus } from "../Enum/constEnum";

export interface LeaveUpdate {
    userLeaveId: number;
    userId: number;
    status: LeaveStatus;
}