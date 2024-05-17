using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Application.Interfaces;
public interface IUnitOfWork
{
    Task SaveChangesAsync();
    IGenericRepository<T> Repository<T>() where T : class;
}
