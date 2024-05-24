export interface UserLeave {
    userId: number;
    name: string;
    leaveTypeName: string;
    fromDate: string;
    toDate: string;
    comments: string | null;
    status: string;
}