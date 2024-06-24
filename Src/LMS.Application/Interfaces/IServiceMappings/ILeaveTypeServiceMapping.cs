using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Helpers.Pagination;


namespace LMS.Application.Interfaces.IServiceMappings
{
    public interface ILeaveTypeServiceMapping : IGenericServiceAsync<LeaveType, LeaveTypeDto>
    {
        public Task<PagedListResult<LeaveTypeDto>> GetAllLeaveTypeSearch(UserParams userParams);
    }
}