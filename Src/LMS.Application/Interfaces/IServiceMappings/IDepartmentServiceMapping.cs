using LMS.Application.DTOs;
using LMS.Application.Helpers.Pagination;
using LMS.Application.Interfaces.IServices;
using LMS.Domain.Entities;

namespace LMS.Application.IServiceMappings;

public interface IDepartmentServiceMapping : IGenericServiceAsync<Department, DepartmentDto>
{
    Task<PagedListResult<DepartmentDto>> GetAllDepartmentSearch(UserParams userParams);

}
