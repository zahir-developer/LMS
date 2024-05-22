using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces;


namespace LMS.Application.Interfaces.ServiceMappings
{
    public interface ILeaveTypeService : IReadServiceAsync<LeaveType, LeaveTypeDto>
    {
        
    }
}