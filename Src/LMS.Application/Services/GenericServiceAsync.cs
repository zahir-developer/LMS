using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;
using LMS.Application.Interfaces;


namespace LMS.Application.Services
{
    public class GenericServiceAsync<TEntity, TDto> : ReadServiceAsync<TEntity, TDto>, IGenericServiceAsync<TEntity, TDto>
        where TEntity : class 
        where TDto : class
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericServiceAsync(IGenericRepository<TEntity> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(TDto dto, CancellationToken cancellationToken)
        {
            await _genericRepository.AddAsync(_mapper.Map<TEntity>(dto));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _genericRepository.DeleteByIdAsync(id);
        }

        public async Task UpdateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _genericRepository.UpdateAsync(entity);
        }

        public async Task<bool> SaveAsync()
        {
            return await _genericRepository.SaveAsync();
        }
    }
}
