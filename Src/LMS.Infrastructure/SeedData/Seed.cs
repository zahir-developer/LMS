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
namespace LMS.Infrastructure.SeedData
{
    public static class Seed
    {
        public static async Task SeedRoleData(LMSDbContext context)
        {
            
            if (await context.Role.AnyAsync()) return;

            var roleData = await File.ReadAllTextAsync("Data/RoleSeedData.json");

            //console.log("roleData:\n \n ");
            //console.log(roleData);

            //var options = new JsonSerializeOptions(PropertyNameCaseInsensitive = true);

            var roles = JsonSerializer.Deserialize<List<Role>>(roleData);

            // if (!roles.Any())
            //     throw new Exception("No roles found");

            foreach (var role in roles)
            {
                context.Role.Add(role);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedUserData(LMSDbContext context)
        {
            if (await context.User.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            //var options = new JsonSerializeOptions(PropertyNameCaseInsensitive = true);

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("P@ssw0rd"));
                user.PasswordSalt = hmac.Key;

                context.User.Add(user);
            }

            await context.SaveChangesAsync();
            
        }

        public static async Task SeedLeaveType(LMSDbContext context)
        {
            if (await context.LeaveType.AnyAsync()) return;

            var data = await File.ReadAllTextAsync("Data/LeaveTypeSeedData.json");

            //var options = new JsonSerializeOptions(PropertyNameCaseInsensitive = true);

            var records = JsonSerializer.Deserialize<List<LeaveType>>(data);

            // if (!roles.Any())
            //     throw new Exception("No roles found");

            foreach (var r in records)
            {
                context.LeaveType.Add(r);
            }

            await context.SaveChangesAsync();
        }
    }

}