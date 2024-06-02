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
using LMS.Domain.Entities;
using LMS.Infrastructure.Repository;
using AutoMapper;
namespace LMS.Infrastructure.Repository;


public class UserLeaveRepository(LMSDbContext dbContext, IMapper mapper) : IUserLeaveRepository
{
    public async Task<IEnumerable<UserLeave>> GetAllUserLeaveList(int departmentId)
    {
        var userLeaves = dbContext.UserLeave.Include(s=>s.User).ThenInclude(s=>s.Department).Include(s=>s.LeaveType).Where(s=>s.User.DepartmentId == departmentId);

        return userLeaves;
    }
}
