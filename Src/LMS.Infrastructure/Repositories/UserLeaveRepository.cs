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

public class UserLeaveRepository(LMSDbContext dbContext, IMapper mapper) : GenericRepository<UserLeave>(dbContext), IUserLeaveRepository
{
    public async Task<List<UserLeave>> GetAllUserLeaveAsync()
    {
        var userLeaves = await dbContext.UserLeave.Include(s=>s.User).Include(s=>s.LeaveType).ToListAsync();

        return userLeaves;
    }
}
