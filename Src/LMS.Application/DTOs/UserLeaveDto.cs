namespace LMS.Application.DTOs;
public class UserLeaveDto
{
    public int Id { get; set;}
    public int UserId { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Comments { get; set; }
    public int Status { get; set; }
}
