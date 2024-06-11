using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using LMS.Domain.Entities;

namespace LMS.Application.Interfaces.IRepository;

public interface IUserRepository
{
    Task<User> GetUserRoleDetailsAsync(int userId);
    Task<IQueryable<User>> GetAllUserAsync();
    Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>>? filter = null, IOrderedQueryable<User> orderBy = null, string includeProperties = "");
}