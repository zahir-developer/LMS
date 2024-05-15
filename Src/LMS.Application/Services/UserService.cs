using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using LMS.Domain.Entities;
using LMS.Application.DTO;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces;

namespace LMS.Application.Services;

public class UserService : ReadServiceAsync<User, UserDto>, IUserService
{
    public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {

    }
}