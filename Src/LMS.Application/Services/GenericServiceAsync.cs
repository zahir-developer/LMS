using System.Linq.Expressions;
using AutoMapper;

using LMS.Application.Interfaces.IServices;
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
        }

        public async Task AddRangeAsync(List<TDto> dtos, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Repository<TEntity>().AddRangeAsync(_mapper.Map<List<TEntity>>(dtos), cancellationToken);
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

        public bool SaveChangesAsync()
        {
            return _unitOfWork.SaveChangesAsync().Result;
        }
    }
}
