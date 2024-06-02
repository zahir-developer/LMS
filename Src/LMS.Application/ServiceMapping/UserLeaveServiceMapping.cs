using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTOs;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IRepository;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces;
using LMS.Application.Constants;
using LMS.Domain.Entities;
using AutoMapper;
using System.Linq;
using System;

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
    public List<UserLeaveListDto> GetAllUserLeaveList(int departmentId, int userId = 0)
    {
        var result = this._unitOfWork.UserLeaveRepository.GetAllUserLeaveList(departmentId).Result;
        var userLeaveResult = (from u in result
                               select new UserLeaveListDto()
                               {
                                   Id = u.Id,
                                   UserId = u.UserId,
                                   Name = u.User?.FirstName + " " + u.User?.LastName,
                                   LeaveTypeName = u.LeaveType?.LeaveTypeName,
                                   FromDate = u.FromDate,
                                   ToDate = u.ToDate,
                                   Comments = u.Comments,
                                   StatusName = ((ConstEnum.LeaveStatus)u.Status).ToString()
                               });
        if (userId > 0)
        {
            userLeaveResult = userLeaveResult.Where(u => u.UserId == userId);
        }
        return userLeaveResult.ToList();
    }
}