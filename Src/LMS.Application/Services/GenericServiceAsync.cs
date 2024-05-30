using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

using LMS.Application.Interfaces.IServices;
using LMS.Application.Interfaces.IRepository;
using LMS.Application.Interfaces;

namespace LMS.Application.Services
{
    public class GenericServiceAsync<TEntity, TDto> : IGenericServiceAsync<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GenericServiceAsync(IUnitOfWork unitOfWork, IMapper mapper)
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

        public async Task<TDto> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id);

            if (result != null)
            {
                return _mapper.Map<TDto>(result);
            }

            return null;
        }

        public async Task<List<TDto>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
             var result = await _unitOfWork.Repository<TEntity>().GetAsync(filter);

            if (result != null)
            {
                return _mapper.Map<List<TDto>>(result);
            }

            return null;
        }

        public async Task AddAsync(TDto dto, CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository<TEntity>().AddAsync(_mapper.Map<TEntity>(dto));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.Repository<TEntity>().DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _unitOfWork.Repository<TEntity>().UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
