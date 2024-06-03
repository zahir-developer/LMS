namespace LMS.Application.DTOs;
public class UserLeaveReportDto
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LeaveTypeId { get; set; }
    public string LeaveType { get; set; }
    public int TotalLeave { get; set; }
    public int TotalLeaveTaken { get; set; }
    public int TotalLeaveRemaining { get { return this.TotalLeave - this.TotalLeaveTaken; } }
}
