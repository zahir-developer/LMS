using AutoMapper;
using AutoMapper.QueryableExtensions;
using LMS.Application.DTOs;
using LMS.Application.Helpers.Pagination;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Services;
using LMS.Domain.Entities;

namespace LMS.Application.ServiceMappings;

public class DepartmentServiceMapping : GenericServiceAsync<Department, DepartmentDto>, IDepartmentServiceMapping
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartmentServiceMapping(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedListResult<DepartmentDto>> GetAllDepartmentSearch(UserParams userParams)
    {
        var departments = _unitOfWork.Repository<Department>().GetAllQueryable();

        if(!string.IsNullOrEmpty(userParams.SearchText))
        {
            departments = departments.Where(
                            q => q.DepartmentName.ToLower().Contains(userParams.SearchText.ToLower()) ||
                            q.Description.ToLower().Contains(userParams.SearchText.ToLower()));
        }

        var pagedList = await PagedList<DepartmentDto>.CreateAsync(departments.ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);
        
        return new PagedListResult<DepartmentDto>(pagedList, userParams.PageNumber, userParams.PageSize,
                                                   pagedList.TotalCount, pagedList.TotalPages, userParams.SearchText);


    }
}
