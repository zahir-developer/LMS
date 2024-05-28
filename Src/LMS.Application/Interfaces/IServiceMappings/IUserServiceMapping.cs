using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;

namespace LMS.Application.IServiceMappings
{
    public interface IUserServiceMapping : IGenericServiceAsync<User, UserDto>
    {
        UserDto GetUserByEmail(string emailId);
        Task<LoginResultDto> GetUserRolePrvilegeDetail(int userId);
        Task<List<UserListDto>> GetAllUserListAsync();
    }
}