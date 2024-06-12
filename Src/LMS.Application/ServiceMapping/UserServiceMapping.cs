
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTOs;
using System.Linq.Expressions;
using AutoMapper;
using LMS.Application.Helpers.Pagination;
using static LMS.Application.Constants.ConstEnum;


namespace LMS.Application.ServiceMappings
{
    public class UserServiceMapping : GenericServiceAsync<User, UserDto>, IUserServiceMapping
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserServiceMapping(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public UserDto GetUserByEmail(string emailId)
        {
            List<UserDto> userList = new List<UserDto>();

            Expression<Func<User, bool>> exp = s => s.Email == emailId;

            var user = _unitOfWork.UserRepository.GetAsync(exp).Result.ToList().FirstOrDefault();

            var result = _mapper.Map<UserDto>(user);

            return result;
        }

        public async Task<LoginResultDto> GetUserRolePrvilegeDetail(int userId)
        {
            var userRolePrivileges = _unitOfWork.UserRepository.GetUserRoleDetailsAsync(userId).Result;

            var result = _mapper.Map<LoginResultDto>(userRolePrivileges);

            return result;
        }

        public async Task<PagedListResult<UserListDto>> GetAllUserListAsync(UserParams userParams)
        {
            PageListConfig pageConfig = new PageListConfig();
            var userList = _unitOfWork.UserRepository.GetAllUserAsync().Result;

            var usersResult = (from u in userList
                               select new UserListDto()
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   RoleId = u.RoleId,
                                   RoleName = u.Role.RoleName,
                                   DepartmentId = u.Department.Id,
                                   DepartmentName = u.Department.DepartmentName
                               }).AsQueryable();

            if (!string.IsNullOrEmpty(userParams.SearchText))
            {
                userParams.SearchText = userParams.SearchText.Trim().ToLower();
                usersResult = usersResult.Where(s =>
                               s.FirstName.ToLower().Contains(userParams.SearchText) ||
                               s.LastName.ToLower().Contains(userParams.SearchText) ||
                               s.Email.ToLower().Contains(userParams.SearchText) ||
                               s.RoleName.ToLower().Contains(userParams.SearchText) ||
                               s.DepartmentName.ToLower().Contains(userParams.SearchText))
                               .AsQueryable();
            }

            if (userParams.SortBy != null)
            {
                if (userParams.SortDir.Equals(SortDirection.ASC))
                {
                    switch (userParams.SortBy.ToLower())
                    {
                        case "firstname":
                            usersResult = usersResult.OrderBy(s => s.FirstName);
                            break;
                        case "lastname":
                            usersResult = usersResult.OrderBy(s => s.LastName);
                            break;
                        case "email":
                            usersResult = usersResult.OrderBy(s => s.Email);
                            break;
                        default:
                            usersResult = usersResult.OrderBy(s => s.Id);
                            break;
                    }
                }
                else if (userParams.SortDir.Equals(SortDirection.DESC))
                {
                    switch (userParams.SortBy.ToLower())
                    {
                        case "firstname":
                            usersResult = usersResult.OrderByDescending(s => s.FirstName);
                            break;
                        case "lastname":
                            usersResult = usersResult.OrderByDescending(s => s.LastName);
                            break;
                        case "email":
                            usersResult = usersResult.OrderByDescending(s => s.Email);
                            break;
                        default:
                            usersResult = usersResult.OrderByDescending(s => s.Id);
                            break;
                    }
                }
            }

            var pagedList = PagedList<UserListDto>.CreateAsync(usersResult, userParams.PageNumber, userParams.PageSize).Result;

            pageConfig.PageNumber = userParams.PageNumber;
            pageConfig.PageSize = userParams.PageSize;
            pageConfig.TotalItems = pagedList.TotalCount;
            pageConfig.TotalPages = pagedList.TotalPages;
            pageConfig.SortBy = userParams.SortBy;
            pageConfig.SortDir = userParams.SortDir.ToString();
            return new PagedListResult<UserListDto>(pagedList, pageConfig, userParams.SearchText);
        }
    }
}