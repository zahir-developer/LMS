using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Application.Interfaces.IRepository;

namespace LMS.Application.Interfaces;
public interface IUnitOfWork
{
    Task SaveChangesAsync();
    IGenericRepository<T> Repository<T>() where T : class;
}
