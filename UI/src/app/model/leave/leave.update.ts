import { LeaveStatus } from "../Enum/constEnum";

export interface LeaveUpdate {
    id: number;
    userId: number;
    status: LeaveStatus;
}