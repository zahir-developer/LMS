using Microsoft.EntityFrameworkCore;
using LMS.Application.Interfaces.IRepository;
using LMS.Infrastructure.Database;
using LMS.Domain.Entities;
using AutoMapper;
using static LMS.Application.Constants.ConstEnum;
using LMS.Application.DTOs;
namespace LMS.Infrastructure.Repository;


public class UserLeaveRepository(LMSDbContext dbContext, IMapper mapper) : IUserLeaveRepository
{
    public async Task<IEnumerable<UserLeave>> GetAllUserLeaveList(int departmentId)
    {
        var userLeaves = dbContext.UserLeave.Include(s => s.User).ThenInclude(s => s.Department).Include(s => s.LeaveType).AsQueryable();

        if (departmentId > 0)
            userLeaves = userLeaves.Where(s => s.User.DepartmentId == departmentId).AsQueryable();

        return userLeaves;
    }

    public async Task<List<UserLeaveReportDto>> GetUserLeaveReport(int departmentId = 0)
    {
        var userLeaveReport = dbContext.UserLeave.Include(s => s.LeaveType).Include(s => s.User).AsQueryable()
          .Where(s => s.Status != (int)LeaveStatus.Rejected && s.Status != (int)LeaveStatus.Cancelled).AsQueryable();

        if (departmentId > 0)
            userLeaveReport = userLeaveReport.Where(s => s.User.DepartmentId == departmentId).AsQueryable();

        var userLeaveGroup = userLeaveReport.GroupBy(g => new { g.LeaveTypeId, g.User.Id, g.User.FirstName, g.LeaveType.LeaveTypeName })
        .Select(s => new UserLeaveReportDto()
        {
            LeaveTypeId = s.Key.LeaveTypeId,
            UserId = s.Key.Id,
            Name = s.Key.FirstName,
            LeaveType = s.Key.LeaveTypeName,
            TotalLeaveTaken = s.Count(c => c.Id > 0),
            TotalLeave = s.Max(g => g.LeaveType.MaxLeaveCount)
        }).ToList();

        var users = dbContext.User.Include(s => s.Role).Where(s => s.DepartmentId == departmentId && s.Role.RoleName != Roles.Manager.ToString() || departmentId == 0).Select(s => new { s.Id, s.FirstName, s.LastName }).ToList();

        foreach (var user in users)
        {
            var uLeaveTypes = dbContext.LeaveType.ToList().Where(u => !userLeaveGroup.Exists(s => s.LeaveTypeId == u.Id && s.UserId == user.Id));

            if (uLeaveTypes.Any())
                foreach (var leaveType in uLeaveTypes)
                    userLeaveGroup.Add(new UserLeaveReportDto()
                    {
                        UserId = user.Id,
                        LeaveTypeId = leaveType.Id,
                        //UserId = s.Key.Id,
                        Name = user.FirstName,
                        LeaveType = leaveType.LeaveTypeName,
                        TotalLeaveTaken = 0,
                        TotalLeave = leaveType.MaxLeaveCount
                    });
        }

        return userLeaveGroup.OrderBy(s => s.UserId).ToList();
    }

    public async Task<UserLeaveDto?> GetUserLeaveDetail(int userLeaveId)
    {
        var userLeave = await dbContext.UserLeave
            .Include(s => s.LeaveType)
            .Include(u => u.User)
            .Where(s => s.Id == userLeaveId)
            .Select(s => new UserLeaveDto()
            {
                FirstName = s.User.FirstName,
                Email = s.User.Email,
                FromDate = s.FromDate,
                ToDate = s.ToDate,
                LeaveTypeName = s.LeaveType.LeaveTypeName,
                Status = s.Status
            }).FirstOrDefaultAsync();

        return userLeave;
    }

}
