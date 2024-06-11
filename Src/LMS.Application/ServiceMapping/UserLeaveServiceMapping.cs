using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Domain.Entities;
using AutoMapper;

namespace LMS.Application.ServiceMappings;
public class UserLeaveServiceMapping : GenericServiceAsync<UserLeave, UserLeaveDto>, IUserLeaveServiceMapping
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper mapper;

    public UserLeaveServiceMapping(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this._unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public List<UserLeaveListDto> GetAllUserLeaveList(int departmentId = 0, int userId = 0)
    {
        var result = this._unitOfWork.UserLeaveRepository.GetAllUserLeaveList(departmentId).Result;

        if (userId > 0)
        {
            result = result.Where(u => u.UserId == userId).ToList();
        }

        var userLeaveResult = mapper.Map<List<UserLeaveListDto>>(result);

        return userLeaveResult;
    }

    public List<UserLeaveReportDto> GetUserLeaveReport(int departmentId, int userId)
    {
        var report = this._unitOfWork.UserLeaveRepository.GetUserLeaveReport(departmentId).Result;

        if (userId > 0)
            report = report.Where(u => u.UserId == userId).ToList();
            
        return report;
    }
}