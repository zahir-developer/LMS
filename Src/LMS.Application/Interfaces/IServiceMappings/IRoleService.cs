using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IServices;


namespace LMS.Application.Interfaces.IServiceMappings
{
    public interface IRoleService : IGenericServiceAsync<Role, RoleDto>
    {
        Task<List<RoleDto>> GetAllRolesAsync();
    }
}