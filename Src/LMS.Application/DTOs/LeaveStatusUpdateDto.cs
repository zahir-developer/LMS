 
namespace LMS.Application.DTOs;
using LMS.Application.Constants;
public class LeaveStatusUpdateDto
{
    public int Id { get; set;}
    public int UserId { get; set; }
    public ConstEnum.LeaveStatus Status { get; set; }
}
