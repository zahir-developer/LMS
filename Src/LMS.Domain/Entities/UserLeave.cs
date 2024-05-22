namespace LMS.Domain.Entities;
public class UserLeave : BaseEntity
{
    public int UserId { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Comments { get; set; }
    public virtual LeaveType LeaveType { get; set; }
}
