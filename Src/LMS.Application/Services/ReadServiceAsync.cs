using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Core;
using AutoMapper;

using LMS.Application.Interfaces;
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
            try
            {
                var result = await _genericRepository.GetAllAsync();

                if (result.Any())
                {
                    return _mapper.Map<IEnumerable<TDto>>(result);
                }
            }
            catch (Exception ex)
            {
                var message = $"Error retrieving all {typeof(TDto).Name}s";

                throw ex;
            }

            return null;
        }
    }
}