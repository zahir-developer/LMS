using System.Linq.Expressions;

namespace LMS.Application.Interfaces.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> GetAllQueryable();
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, IOrderedQueryable<T> orderBy = null, string includeProperties = "");
    Task<T?> GetByIdAsync(int Id, CancellationToken cancellationToken = default);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int Id, CancellationToken cancellationToken = default);
}