export interface UserLeaveAdd {
    leaveTypeId: number;
    userId: number | undefined;
    fromDate: Date;
    toDate: Date;
    comments: string | undefined;
}