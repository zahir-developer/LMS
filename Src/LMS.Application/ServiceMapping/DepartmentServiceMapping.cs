using AutoMapper;
using LMS.Application.DTOs;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Services;
using LMS.Domain.Entities;

namespace LMS.Application.ServiceMappings;

public class DepartmentServiceMapping : GenericServiceAsync<Department, DepartmentDto>, IDepartmentServiceMapping
{
    public DepartmentServiceMapping(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
