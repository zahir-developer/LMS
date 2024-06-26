using LMS.Application.Interfaces.IRepository;

namespace LMS.Application.Interfaces;
public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
    IGenericRepository<T> Repository<T>() where T : class;
    IUserRepository UserRepository { get; }
    IUserLeaveRepository UserLeaveRepository { get; }
    bool HasChanges();
    
}
