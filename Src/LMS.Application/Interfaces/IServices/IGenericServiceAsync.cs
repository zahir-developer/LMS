using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Application.Interfaces.IServices;
using System.Linq.Expressions;

using LMS.Domain.Entities;

namespace LMS.Application.Interfaces.IServices;
public interface IGenericServiceAsync<TEntity, TDto> where TEntity : class where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> GetByIdAsync(int id);
    Task<List<TDto>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task<bool> AddAsync(TDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(TDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(int Id);
}