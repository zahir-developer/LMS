using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;
using LMS.Infrastructure.Database;
using LMS.Infrastructure.Repository;
using LMS.Domain.Entities;
using AutoMapper;

namespace LMS.Infrastructure.Repository;

public class UserRepository(LMSDbContext dbContext, IMapper mapper) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<User> GetUserRoleDetailsAsync(int userId)
    {
        var userRoleDetails = dbContext.User.Include(s=>s.Role).ThenInclude(s=>s.RolePrivilege).Where(s=>s.Id == userId).FirstOrDefault();

        return userRoleDetails;
    }
    public async Task<IQueryable<User>> GetAllUserAsync()
    {
        var userRoleDetails = dbContext.User.Include(s=>s.Role).AsQueryable();

        return userRoleDetails;
    }

    

    public async Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>>? filter = null, IOrderedQueryable<User> orderBy = null, string includeProperties = "")
    {
        IQueryable<User> query;

        if (filter != null)
        {
            query = dbContext.Set<User>().Where(filter);
        }
        else
        {
            query = dbContext.Set<User>();
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }
}
