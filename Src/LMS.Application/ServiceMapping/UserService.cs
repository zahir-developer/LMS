
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
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
    public class UserService : GenericServiceAsync<User, UserDto>, IUserService
    {
        private readonly IGenericRepository<User> _genUserRepo;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> genericRepository, IUserRepository userRepository, IMapper mapper) : base(genericRepository, mapper)
        {
            _genUserRepo = genericRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto GetUserByEmail(string emailId)
        {
            List<UserDto> userList = new List<UserDto>();

            Expression<Func<User, bool>> exp = s => s.Email == emailId;

            var user = _genUserRepo.GetAsync(exp).Result.ToList().FirstOrDefault();

            var result = _mapper.Map<UserDto>(user);

            return result;
        }

        public async Task<LoginResultDto> GetUserRolePrvilegeDetail(int userId)
        {
            var userRolePrivileges = _userRepository.GetUserRoleDetailsAsync(userId).Result;

            var result = _mapper.Map<LoginResultDto>(userRolePrivileges);

            return result;
        }
    }
}