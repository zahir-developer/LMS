using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Core;
using AutoMapper;

using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;
using LMS.Domain.Entities;



namespace LMS.Application.Services
{
    public class ReadServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
    where TEntity : class
    where TDto : class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;

        public ReadServiceAsync(IGenericRepository<TEntity> genericRepository, IMapper mapper) : base()
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var result = await _genericRepository.GetAllAsync();

            if (result.Any())
            {
                return _mapper.Map<IEnumerable<TDto>>(result);
            }

            return null;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var result = await _genericRepository.GetByIdAsync(id);

            if (result != null)
            {
                return _mapper.Map<TDto>(result);
            }

            return null;
        }
    }
}