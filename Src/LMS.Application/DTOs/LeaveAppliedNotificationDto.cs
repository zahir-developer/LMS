using static LMS.Application.Constants.ConstEnum;

namespace LMS.Application;

public class LeaveAppliedNotificationDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int LeaveTypeId { get; set; }
    public int DepartmentId { get; set; }
}
