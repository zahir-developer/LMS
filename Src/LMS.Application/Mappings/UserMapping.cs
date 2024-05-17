using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LMS.Domain.Entities;
using AutoMapper;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTO;


namespace LMS.Application.Mappings
{
    public class UserService : GenericServiceAsync<User, UserDto>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}