
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


namespace LMS.Application.ServiceMappings
{
    public class UserServiceMapping : GenericServiceAsync<User, UserDto>, IUserServiceMapping
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserServiceMapping(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto GetUserByEmail(string emailId)
        {
            List<UserDto> userList = new List<UserDto>();

            Expression<Func<User, bool>> exp = s => s.Email == emailId;

            var user = _userRepository.GetAsync(exp).Result.ToList().FirstOrDefault();

            var result = _mapper.Map<UserDto>(user);

            return result;
        }

        public async Task<LoginResultDto> GetUserRolePrvilegeDetail(int userId)
        {
            var userRolePrivileges = _userRepository.GetUserRoleDetailsAsync(userId).Result;

            var result = _mapper.Map<LoginResultDto>(userRolePrivileges);

            return result;
        }

        public async Task<List<UserListDto>> GetAllUserListAsync()
        {
            var userList = _userRepository.GetAllUserAsync().Result;

            var usersResult = (from u in userList
                               select new UserListDto()
                               {
                                   Id = u.Id,
                                   FirstName = u.FirstName,
                                   LastName = u.LastName,
                                   Email = u.Email,
                                   RoleId = u.RoleId,
                                   RoleName = u.Role.RoleName
                               }).ToList();

            return usersResult;
        }

    }
}