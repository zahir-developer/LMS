using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces;


namespace LMS.Application.Interfaces.ServiceMappings
{
    public interface IUserLeaveServiceMapping : IReadServiceAsync<UserLeave, UserLeaveDto>, IGenericServiceAsync<UserLeave, UserLeaveDto>
    {
        List<UserLeaveListDto> GetAllUserLeaveList();
    }
}