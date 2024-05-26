
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServiceMappings;
using LMS.Application.Interfaces.IRepository;
using LMS.Application.Services;
using LMS.Application.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace LMS.Application.ServiceMappings
{
    public class RoleService : GenericServiceAsync<Role, RoleDto>, IRoleService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            var roles = await base.GetAllAsync();

            var roleResult = (from r in roles
                                select new RoleDto()
                                {
                                    Id = r.Id,
                                    RoleName = r.RoleName,
                                    Description = r.Description,
                                }).ToList();

            return roleResult;
        }
    }
}