
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTOs;
using AutoMapper;

namespace LMS.Application.ServiceMappings
{
    public class LeaveTypeService : GenericServiceAsync<LeaveType, LeaveTypeDto>, ILeaveTypeService 
    {
        public LeaveTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}