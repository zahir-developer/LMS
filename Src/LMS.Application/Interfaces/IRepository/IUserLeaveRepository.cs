using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using LMS.Domain.Entities;

namespace LMS.Application.Interfaces.IRepository;

public interface IUserLeaveRepository
{
    Task<List<UserLeave>> GetAllUserLeaveAsync();
    //void GetAllAsync();
}