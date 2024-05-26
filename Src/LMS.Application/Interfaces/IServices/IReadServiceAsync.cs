using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Application.Interfaces.IServices;
using LMS.Domain.Entities;

namespace LMS.Application.Interfaces.IServices;
public interface IReadServiceAsync<TEntity, TDto> 
where TEntity : class
where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> GetByIdAsync(int id);
}