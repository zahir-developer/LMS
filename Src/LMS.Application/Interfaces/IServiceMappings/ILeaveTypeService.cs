using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces.IServices;


namespace LMS.Application.Interfaces.IServiceMappings
{
    public interface ILeaveTypeService : IGenericServiceAsync<LeaveType, LeaveTypeDto>
    {
        
    }
}