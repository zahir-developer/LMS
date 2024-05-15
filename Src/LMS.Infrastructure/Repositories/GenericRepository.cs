using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken= default)
    {
        return await dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int Id, CancellationToken cancellationToken= default)
    {
        return await dbContext.Set<T>().FindAsync(new[] { Id });
    }

    public async void AddAsync(T entity, CancellationToken cancellationToken= default)
    {
        await dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await SaveAsync();
    }

    public async void UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await SaveAsync();
    }

    public async void DeleteByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entityDelete = await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        dbContext.Set<T>().RemoveRange(entityDelete);
        await SaveAsync();
    }

    private async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
