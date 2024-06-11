
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;
using LMS.Application.Services;
using LMS.Application.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using LMS.Application.Helpers.Pagination;


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
            PagedListResult<UserListDto> paginationHeader;
            var userList = _unitOfWork.UserRepository.GetAllUserAsync().Result;

            var usersResult = (from u in userList
                               select new UserListDto()
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   RoleId = u.RoleId,
                                   RoleName = u.Role.RoleName
                               }).AsQueryable();

            var pagedList =  PagedList<UserListDto>.CreateAsync(usersResult, userParams.PageNumber, userParams.PageSize).Result;

            return new PagedListResult<UserListDto>(pagedList, pagedList.CurrentPage, pagedList.PageSize, pagedList.TotalCount, pagedList.TotalPages);
        }
    }
}