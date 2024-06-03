using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;

namespace LMS.Application.Interfaces.IServiceMappings
{
    public interface IUserLeaveServiceMapping : IGenericServiceAsync<UserLeave, UserLeaveDto>
    {
        List<UserLeaveListDto> GetAllUserLeaveList(int departmentId = 0, int userId = 0);
        List<UserLeaveReportDto> GetUserLeaveReport(int userId = 0);
    }
}