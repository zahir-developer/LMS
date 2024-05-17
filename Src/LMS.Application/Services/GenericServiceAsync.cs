using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using LMS.Application.Interfaces;

namespace LMS.Application.Services
{
    public class GenericServiceAsync<TEntity, TDto> : ReadServiceAsync<TEntity, TDto>, IGenericServiceAsync<TEntity, TDto>
        where TEntity : class 
        where TDto : class
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GenericServiceAsync(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(TDto dto, CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository<TEntity>().AddAsync(_mapper.Map<TEntity>(dto));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.Repository<TEntity>().DeleteByIdAsync(id);
        }

        public async Task UpdateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _unitOfWork.Repository<TEntity>().UpdateAsync(entity);
        }
    }
}
