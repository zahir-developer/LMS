using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces.IServices;

namespace LMS.Application.Interfaces.IServiceMappings
{
    public interface IUserLeaveServiceMapping : IGenericServiceAsync<UserLeave, UserLeaveDto>
    {
        List<UserLeaveListDto> GetAllUserLeaveList(int departmentId = 0, int userId = 0);
        List<UserLeaveReportDto> GetUserLeaveReport(int departmentId = 0, int userId = 0);
    }
}