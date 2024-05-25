using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using LMS.Application.Interfaces;
using LMS.Infrastructure.Database;
using LMS.Domain.Entities;
using AutoMapper;
namespace LMS.Infrastructure.Repositories;

public class UserRepository(LMSDbContext dbContext, IMapper mapper) : GenericRepository<User>(dbContext), IUserRepository
{
    public async Task<User> GetUserRoleDetailsAsync(int userId)
    {
        var userRoleDetails = dbContext.User.Include(s=>s.Role).ThenInclude(s=>s.RolePrivilege).Where(s=>s.Id == userId).FirstOrDefault();

        return userRoleDetails;
    }
}
