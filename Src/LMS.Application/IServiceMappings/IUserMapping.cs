using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LMS.Application.DTO;
using LMS.Domain.Entities;
using LMS.Application.Interfaces;

namespace LMS.Application.IServiceMappings
{
    public interface IUserService : IGenericServiceAsync<User, UserDto>
    {
        
    }
}