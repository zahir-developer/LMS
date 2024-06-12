using AutoMapper;
using AutoMapper.QueryableExtensions;
using LMS.Application.DTOs;
using LMS.Application.Helpers.Pagination;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Services;
using LMS.Domain.Entities;
using static LMS.Application.Constants.ConstEnum;

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
        PageListConfig pageConfig = new PageListConfig();
        var departments = _unitOfWork.Repository<Department>().GetAllQueryable();

        if (!string.IsNullOrEmpty(userParams.SearchText))
        {
            departments = departments.Where(
                            q => q.DepartmentName.ToLower().Contains(userParams.SearchText.ToLower()) ||
                            q.Description.ToLower().Contains(userParams.SearchText.ToLower()));
        }

        if (userParams.SortBy != null)
        {
            if (userParams.SortDir.Equals(SortDirection.ASC))
            {
                switch (userParams.SortBy.ToLower())
                {
                    case "name":
                        departments = departments.OrderBy(q => q.DepartmentName);
                        break;
                    case "description":
                        departments = departments.OrderBy(q => q.Description);
                        break;
                    default:
                        departments = departments.OrderBy(q => q.Id);
                        break;
                }
            }
            else
            {
                if (userParams.SortDir.Equals(SortDirection.DESC))
                {
                    switch (userParams.SortBy.ToLower())
                    {
                        case "name":
                            departments = departments.OrderByDescending(q => q.DepartmentName);
                            break;
                        case "description":
                            departments = departments.OrderByDescending(q => q.Description);
                            break;
                        default:
                            departments = departments.OrderByDescending(q => q.Id);
                            break;
                    }
                }
            }
        }

        var pagedList = await PagedList<DepartmentDto>.CreateAsync(departments.ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);

        pageConfig.PageNumber = userParams.PageNumber;
        pageConfig.PageSize = userParams.PageSize;
        pageConfig.TotalItems = pagedList.TotalCount;
        pageConfig.TotalPages = pagedList.TotalPages;
        pageConfig.SortBy = userParams.SortBy;
        pageConfig.SortDir = userParams.SortDir.ToString();
        return new PagedListResult<DepartmentDto>(pagedList, pageConfig, userParams.SearchText);


    }
}
