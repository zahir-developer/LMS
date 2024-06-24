using static LMS.Application.Constants.ConstEnum;

namespace LMS.Application;

public class LeaveStatusNotificationDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string EmailId { get; set; }
    public string Date { get; set; }
    public LeaveTypeEnum LeaveType { get; set; }
    public LeaveStatus LeaveStatus { get; set; }
    
}
