using Microsoft.EntityFrameworkCore;
using LMS.Domain.Entities;
using LMS.Domain;

namespace LMS.Infrastructure.Database
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions options): base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RolePrivilege> RolePrivilege { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<UserLeave> UserLeave { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}