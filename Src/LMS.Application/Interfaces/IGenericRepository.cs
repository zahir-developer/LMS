using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LMS.Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
    void AddAsync(T entity, CancellationToken cancellationToken);
    void DeleteByIdAsync(int Id, CancellationToken cancellationToken);
}