using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LMS.Domain.Entities;

namespace LMS.Application.Interfaces;
public interface IReadServiceAsync<TEntity, TDto> 
where TEntity : class
where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    //Task<User> GetUserByIdAsync(int Id);
}