
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
        private readonly IGenericRepository<User> _userRepo;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
            _userRepo = genericRepository;
            _mapper = mapper;
        }

        public UserDto GetUserByEmail(string emailId)
        {
            List<UserDto> userList = new List<UserDto>();
            
            Expression<Func<User, bool>> exp = s => s.Email == emailId;

            var user = _userRepo.GetAsync(exp).Result.ToList().FirstOrDefault();

            var result = _mapper.Map<UserDto>(user);

            return result;
        }
    }
}