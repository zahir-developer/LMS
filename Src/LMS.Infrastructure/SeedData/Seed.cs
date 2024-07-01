using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Infrastructure.Database;
using System.Text;
using System.Text.Json;
using LMS.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using LMS.Domain;
namespace LMS.Infrastructure.SeedData
{
    public static class Seed
    {
        public static async Task RoleData(LMSDbContext context)
        {

            if (await context.Role.AnyAsync()) return;

            var roleData = await File.ReadAllTextAsync("Data/RoleSeedData.json");

            //console.log("roleData:\n \n ");
            //console.log(roleData);

            //var options = new JsonSerializeOptions(PropertyNameCaseInsensitive = true);

            var roles = JsonSerializer.Deserialize<List<Role>>(roleData);

            if (roles?.Count() > 0)
            {
                foreach (var role in roles)
                {
                    context.Role.Add(role);
                }
                await context.SaveChangesAsync();
            }
        }

        public static async Task RolePrivilegeData(LMSDbContext context)
        {
            if (await context.RolePrivilege.AnyAsync()) return;

            var RolePrivilegeData = await File.ReadAllTextAsync("Data/RolePrivilegeSeedData.json");

            var RolePrivilege = JsonSerializer.Deserialize<List<RolePrivilege>>(RolePrivilegeData);

            if (RolePrivilege?.Count > 0)
            {
                foreach (var privilege in RolePrivilege)
                {
                    context.RolePrivilege.Add(privilege);
                }
                await context.SaveChangesAsync();
            }

        }

        public static async Task UserData(LMSDbContext context)
        {
            if (await context.User.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            if (users?.Count > 0)
            {
                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssw0rd"));
                    user.PasswordSalt = hmac.Key;

                    context.User.Add(user);
                }
                await context.SaveChangesAsync();
            }

        }

        public static async Task LeaveType(LMSDbContext context)
        {
            if (await context.LeaveType.AnyAsync()) return;

            var data = await File.ReadAllTextAsync("Data/LeaveTypeSeedData.json");

            var records = JsonSerializer.Deserialize<List<LeaveType>>(data);

            if (records?.Count > 0)
            {
                foreach (var r in records)
                {
                    context.LeaveType.Add(r);
                }
            }
            await context.SaveChangesAsync();
        }

        public static async Task Department(LMSDbContext context)
        {
            if (await context.Department.AnyAsync()) return;

            var data = await File.ReadAllTextAsync("Data/DepartmentSeedData.json");

            var records = JsonSerializer.Deserialize<List<Department>>(data);

            if (records?.Count > 0)
            {
                foreach (var r in records)
                {
                    context.Department.Add(r);
                }
            }
            await context.SaveChangesAsync();
        }

        public static async Task Holiday(LMSDbContext context)
        {
            if (await context.Holiday.AnyAsync()) return;

            var data = await File.ReadAllTextAsync("Data/HolidaySeedData.json");

            var records = JsonSerializer.Deserialize<List<Holiday>>(data);

            if (records?.Count > 0)
            {
                foreach (var r in records)
                {
                    context.Holiday.Add(r);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}