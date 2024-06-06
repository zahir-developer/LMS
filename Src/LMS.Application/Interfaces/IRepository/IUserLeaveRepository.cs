using LMS.Application.DTOs;
using LMS.Domain.Entities;

namespace LMS.Application.Interfaces.IRepository;

public interface IUserLeaveRepository
{
    Task<IEnumerable<UserLeave>> GetAllUserLeaveList(int departmentId);
    Task<List<UserLeaveReportDto>> GetUserLeaveReport(int departmentId);
}