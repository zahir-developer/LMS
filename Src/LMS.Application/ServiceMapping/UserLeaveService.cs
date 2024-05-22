
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Services;
using LMS.Application.DTOs;
using LMS.Application.Interfaces.ServiceMappings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace LMS.Application.ServiceMappings
{
    public class UserLeaveService : GenericServiceAsync<UserLeave, UserLeaveDto>, IUserLeaveService 
    {
        private readonly IGenericRepository<UserLeave> _userRepo;
        private readonly IMapper _mapper;
        public UserLeaveService(IGenericRepository<UserLeave> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
            _userRepo = genericRepository;
            _mapper = mapper;
        }
    }
}