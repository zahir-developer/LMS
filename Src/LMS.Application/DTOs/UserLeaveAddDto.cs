namespace LMS.Application.DTOs;
public class UserLeaveAddDto
{
    public int LeaveTypeId { get; set; }
    public int UserId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Comments { get; set; }
    public int Status { get; set; }
}
