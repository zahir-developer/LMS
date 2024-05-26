import { LeaveStatus } from "../Enum/constEnum";

export interface LeaveUpdate {
    Id: number;
    userId: number;
    status: LeaveStatus;
}