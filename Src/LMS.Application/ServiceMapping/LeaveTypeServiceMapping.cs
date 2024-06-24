
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTOs;
using AutoMapper;
using LMS.Application.Helpers.Pagination;
using static LMS.Application.Constants.ConstEnum;
using AutoMapper.QueryableExtensions;

namespace LMS.Application.ServiceMappings
{
    public class LeaveTypeServiceMapping : GenericServiceAsync<LeaveType, LeaveTypeDto>, ILeaveTypeServiceMapping
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveTypeServiceMapping(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<PagedListResult<LeaveTypeDto>> GetAllLeaveTypeSearch(UserParams userParams)
        {
            PageListConfig pageConfig = new PageListConfig();
            var leaveTypes = _unitOfWork.Repository<LeaveType>().GetAllQueryable();

            if (!string.IsNullOrEmpty(userParams.SearchText))
            {
                leaveTypes = leaveTypes.Where(
                                q => q.LeaveTypeName.ToLower().Contains(userParams.SearchText.ToLower()) ||
                                q.Description.ToLower().Contains(userParams.SearchText.ToLower()));
            }

            if (userParams.SortBy != null)
            {
                if (userParams.SortDir.Equals(SortDirection.ASC))
                {
                    switch (userParams.SortBy.ToLower())
                    {
                        case "name":
                            leaveTypes = leaveTypes.OrderBy(q => q.LeaveTypeName);
                            break;
                        case "description":
                            leaveTypes = leaveTypes.OrderBy(q => q.Description);
                            break;
                        case "maxleavecount":
                            leaveTypes = leaveTypes.OrderBy(q => q.Description);
                            break;
                        default:
                            leaveTypes = leaveTypes.OrderBy(q => q.Id);
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
                                leaveTypes = leaveTypes.OrderByDescending(q => q.LeaveTypeName);
                                break;
                            case "description":
                                leaveTypes = leaveTypes.OrderByDescending(q => q.Description);
                                break;
                            case "maxleavecount":
                                leaveTypes = leaveTypes.OrderByDescending(q => q.Description);
                                break;
                            default:
                                leaveTypes = leaveTypes.OrderByDescending(q => q.Id);
                                break;
                        }
                    }
                }
            }

            var pagedList = await PagedList<LeaveTypeDto>.CreateAsync(leaveTypes.ProjectTo<LeaveTypeDto>(_mapper.ConfigurationProvider), userParams.PageNumber, userParams.PageSize);

            pageConfig.PageNumber = userParams.PageNumber;
            pageConfig.PageSize = userParams.PageSize;
            pageConfig.TotalItems = pagedList.TotalCount;
            pageConfig.TotalPages = pagedList.TotalPages;
            pageConfig.SortBy = userParams.SortBy;
            pageConfig.SortDir = userParams.SortDir.ToString();
            return new PagedListResult<LeaveTypeDto>(pagedList, pageConfig, userParams.SearchText);
        }
    }
}