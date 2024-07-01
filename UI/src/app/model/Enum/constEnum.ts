export enum Confirm
{
    Yes = 'Yes',
    No = 'No'
}

export enum Roles
{
    Admin = 1,
    Employee = 2,
    Manager = 3,
}

export enum LeaveStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
    Cancelled = 3
}

export enum LeaveStatusText
{
    Pending = "Pending",
    Approved = "Approved",
    Rejected = "Rejected",
    Cancel = "Cancelled"
}

export enum AppModule
{
    Department = "Department",
    LeaveType = "Leave Type"
}

export enum AppText
{
    /*Manage leaves: Begin*/
    ApproveConfirmation = "Confirm leave approve action.!",
    RejectConfirmation = "Confirm leave reject action.!",
    CancelConfirmation = "Confirm leave cancel action.!",
    /*Manage leaves: Ends*/

    /*Common*/
    ForbiddenAction = "You are not allowed to perform this action !",
    DeleteConfirmation = "Are you sure proceed to delete ?",
    DeleteSuccess = "Record deleted successfully !",

    /*User*/
    UserUpdateSuccess = "User updated successfully !",
    UserCreatedSuccess = "User created successfully !",
    EmailExists = "Email already exists!",
    EmailNotExists = "Email available to crete !",

    /*Department*/
    AddDepartmentSuccess = 'Department added successfully !',
    UpdateDepartmentSuccess = 'Department updated successfully !',
    DeleteDepartmentSuccess = 'Department deleted successfully !',

    /*Leave Type*/
    AddLeaveTypeSuccess = 'LeaveType added successfully !',
    UpdateLeaveTypeSuccess = 'LeaveType updated successfully !',
    DeleteLeaveTypeSuccess = 'LeaveType deleted successfully !'
}

