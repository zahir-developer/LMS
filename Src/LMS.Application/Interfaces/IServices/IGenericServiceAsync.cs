using System.Linq.Expressions;

namespace LMS.Application.Interfaces.IServices;
public interface IGenericServiceAsync<TEntity, TDto> where TEntity : class where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> GetByIdAsync(int id);
    Task<List<TDto>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task AddAsync(TDto dto, CancellationToken cancellationToken = default);
    Task AddRangeAsync(List<TDto> dtos, CancellationToken cancellationToken = default);
    Task UpdateAsync(TDto dto, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int Id);
    bool SaveChangesAsync();
}