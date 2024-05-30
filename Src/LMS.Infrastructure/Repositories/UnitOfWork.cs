using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Infrastructure.Database;
using LMS.Application.Interfaces;
using LMS.Application.Interfaces.IRepository;
using AutoMapper;

namespace LMS.Infrastructure.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly LMSDbContext _dbContext;
    private readonly IMapper _mapper;

    public UnitOfWork(LMSDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        return new GenericRepository<T>(_dbContext);
    }

    public IUserRepository UserRepository => new UserRepository(_dbContext, _mapper);

    public IUserLeaveRepository UserLeaveRepository => new UserLeaveRepository(_dbContext, _mapper);

    public bool HasChanges()
    {
        return _dbContext.ChangeTracker.HasChanges();
    }


}
