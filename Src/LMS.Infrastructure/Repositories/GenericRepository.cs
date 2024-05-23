using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using LMS.Application.Interfaces;
using LMS.Infrastructure.Database;

namespace LMS.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly LMSDbContext dbContext;
    public GenericRepository(LMSDbContext context)
    {
        dbContext = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, IOrderedQueryable<T> orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query;

        if (filter != null)
        {
            query = dbContext.Set<T>().Where(filter);
        }
        else
        {
            query = dbContext.Set<T>();
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }


    public async Task<T?> GetByIdAsync(int Id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().FindAsync(new[] { Id });
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        return await SaveAsync();
    }

    public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entityDelete = await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        dbContext.Set<T>().RemoveRange(entityDelete);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }
}
