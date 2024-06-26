﻿using System.Linq.Expressions;
using LMS.Domain.Entities;

namespace LMS.Application.Interfaces.IRepository;

public interface IUserRepository
{
    Task<User> GetUserRoleDetailsAsync(int userId);
    Task<User> GetManagerByDepartmentId(int departmentId);
    Task<IQueryable<User>> GetAllUserAsync();
    Task<IEnumerable<User>> GetAsync(Expression<Func<User, bool>>? filter = null, IOrderedQueryable<User> orderBy = null, string includeProperties = "");
}