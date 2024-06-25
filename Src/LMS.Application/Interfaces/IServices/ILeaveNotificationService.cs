using LMS.Application.DTOs;

namespace LMS.Application.Interfaces.IServices;

public interface ILeaveNotificationService
{
    public void LeaveAppliedNotification(LeaveAppliedNotificationDto leaveDto);
    public void LeaveStatusUpdateNofication(LeaveStatusNotificationDto leaveStatusDto);
}
