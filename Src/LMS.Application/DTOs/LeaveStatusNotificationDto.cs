using static LMS.Application.Constants.ConstEnum;

namespace LMS.Application.DTOs;

public class LeaveStatusNotificationDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string LeaveTypeName { get; set; }
    public LeaveStatus Status { get; set; }
    
}
