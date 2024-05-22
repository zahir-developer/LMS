namespace LMS.Domain.Entities;
public class UserLeaveDto
{
    public int LeaveTypeId { get; set; }
    public int UserId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Comments { get; set; }
}
