namespace LMS.Application.Constants
{
    public static class ConstEnum
    {
        public enum LeaveType
        {
            Personal = 1,
            Sick = 2,
            Privilege = 3
        }

        public enum Roles
        {
            Admin = 1,
            User = 2
        }

        public enum LeaveStatus
        {
            Pending = 0,
            Approved = 1,
            Rejected = 2
        }
    }

}