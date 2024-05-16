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
    public class UserMapping : GenericServiceAsync<User, UserDto>, IUserMapping
    {
        public UserMapping(IGenericRepository<User> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}