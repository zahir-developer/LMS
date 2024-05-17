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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReadServiceAsync(IUnitOfWork unitOfWork, IMapper mapper) : base()
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
                var result = await _unitOfWork.Repository<TEntity>().GetAllAsync();

                if (result.Any())
                {
                    return _mapper.Map<IEnumerable<TDto>>(result);
                }

            return null;
        }
    }
}