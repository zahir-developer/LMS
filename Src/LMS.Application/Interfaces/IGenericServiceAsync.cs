using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LMS.Domain.Entities;

namespace LMS.Application.Interfaces;
public interface IGenericServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto> 
where TEntity : class where TDto : class
{
    Task AddAsync(TDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(TDto dto, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int Id);
    Task<bool> SaveAsync();

}