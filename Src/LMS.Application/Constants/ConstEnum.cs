namespace LMS.Application.Constants
{
    public static class ConstEnum
    {
        public const string DATE_FORMAT = "dd-MMM-yyy";
        public enum LeaveTypeEnum
        {
            Personal = 1,
            Sick = 2,
            Privilege = 3,
            LOP = 4
        }

        public enum Roles
        {
            Admin = 1,
            Employee = 2,
            Manager = 3
        }

        public enum LeaveStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2,
            Cancelled = 3,
        }

        public enum SortDirection
        {
            ASC = 0,
            DESC = 1,
        }

        public enum EmailHtmlTemplate
        {
            LeaveApplied = 1,
            LeaveStatusUpdate = 2
        }

        public enum ErrorMessage
        {
            Holiday = 1
        }
    }

}