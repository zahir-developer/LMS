using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMS.Domain.Entities;

namespace LMS.Infrastructure.Database
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions options): base(options)
        {
        }
        public virtual DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set;}
    }
}