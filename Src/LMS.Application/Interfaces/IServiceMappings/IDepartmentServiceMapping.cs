using LMS.Application.DTOs;
using LMS.Application.Interfaces.IServices;
using LMS.Domain.Entities;

namespace LMS.Application.IServiceMappings;

public interface IDepartmentServiceMapping : IGenericServiceAsync<Department, DepartmentDto>
{

}
