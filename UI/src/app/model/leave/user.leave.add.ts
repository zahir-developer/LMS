export interface UserLeaveAdd {
    leaveTypeId: number;
    userId: number | undefined;
    departmentId: number | undefined;
    fromDate: Date;
    toDate: Date;
    comments: string | undefined;
}
