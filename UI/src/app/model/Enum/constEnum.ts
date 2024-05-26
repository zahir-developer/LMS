export enum Confirm
{
    Yes = 'Yes',
    No = 'No'
}

export enum Role
{
    Admin,
    User
}

export enum LeaveStatus
{
    Pending = 0, 
    Approved = 1,
    Rejected = 2
}

export enum LeaveStatusText
{
    Pending = "Pending",
    Approved = "Approved",
    Rejected = "Rejected"
}
export enum AppText
{
    ApproveConfirmation = "Confirm leave approve action.!",
    RejectConfirmation = "Confirm leave reject action.!",
    ForbiddenAction = "You are not allowed to perform this action.|"
}

