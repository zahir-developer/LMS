using LMS.Application.Interfaces.ServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTOs;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces;
using LMS.Application.Constants;
using LMS.Domain.Entities;
using AutoMapper;
using System.Linq;
using System;

namespace LMS.Application.ServiceMappings;
public class UserLeaveServiceMapping : GenericServiceAsync<UserLeave, UserLeaveDto>, IUserLeaveServiceMapping
{
    private readonly IGenericRepository<UserLeave> genericRepo;
    private readonly IUserLeaveRepository userLeaveRepository;
    private readonly IMapper mapper;

    public UserLeaveServiceMapping(IGenericRepository<UserLeave> genericRepo, IUserLeaveRepository userLeaveRepository, IMapper mapper) : base(genericRepo, mapper)
    {
        this.mapper = mapper;
        this.genericRepo = genericRepo;
        this.userLeaveRepository = userLeaveRepository;
    }
    public List<UserLeaveListDto> GetAllUserLeaveList()
    {
        var result = this.userLeaveRepository.GetAllUserLeaveAsync().Result;
        var userLeaveResult = (from u in result
                               select new UserLeaveListDto()
                               {
                                   UserId = u.UserId,
                                   Name = (u.User.FirstName + " " + u.User.LastName),
                                   LeaveTypeName = u.LeaveType?.LeaveTypeName,
                                   FromDate = u.FromDate,
                                   ToDate = u.ToDate,
                                   Comments = u.Comments,
                                   Status = ((ConstEnum.LeaveStatus)u.Status).ToString()
                               }).ToList();

        return userLeaveResult;
    }
}