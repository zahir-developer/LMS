using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Helpers.Pagination;

namespace LMS.Application.IServiceMappings
{
    public interface IUserServiceMapping : IGenericServiceAsync<User, UserDto>
    {
        Task<UserDto> GetUserByEmail(string emailId);
        Task<LoginResultDto> GetUserRolePrvilegeDetail(int userId);
        Task<PagedListResult<UserListDto>> GetAllUserListAsync(UserParams userParams);
        Task<UserDto> GetManagerByDepartment(int DepartmentId);
    }
}