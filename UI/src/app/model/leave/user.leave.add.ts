export interface UserLeaveAdd {
    leaveTypeId: number;
    userId: number;
    fromDate: string;
    toDate: string;
    comments: string | null;
    status: number;
}