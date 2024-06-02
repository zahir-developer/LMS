namespace LMS.Domain.Entities;
public class UserLeave : BaseEntity
{
    public int UserId { get; set; }
    public int LeaveTypeId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string? Comments { get; set; }
    public int Status { get; set; } = 0;
    public virtual User User { get; set; } = new User();
    public virtual LeaveType? LeaveType { get; set; }
}
