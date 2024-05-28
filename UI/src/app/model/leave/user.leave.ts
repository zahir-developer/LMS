export interface UserLeave {
    id: number;
    userId: number;
    name: string;
    leaveTypeName: string;
    fromDate: string;
    toDate: string;
    comments: string | null;
    statusName: string;
}