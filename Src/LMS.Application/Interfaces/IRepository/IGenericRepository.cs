﻿using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LMS.Application.Interfaces.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, IOrderedQueryable<T> orderBy = null, string includeProperties = "");

    Task<T?> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(int Id, CancellationToken cancellationToken = default);
    Task<bool> SaveAsync();
}