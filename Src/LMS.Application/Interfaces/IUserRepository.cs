using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using LMS.Domain.Entities;

namespace LMS.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserRoleDetailsAsync(int userId);
}