using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Infrastructure.Database;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IRepository;

namespace LMS.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly LMSDbContext _dbContext;

    public UnitOfWork(LMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        return new GenericRepository<T>(_dbContext);
    }
}