using LMS.API.Extensions;
using LMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using LMS.Infrastructure.SeedData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddIdentityServices(builder.Configuration);

// builder.Services.AddDbContext<LMSDbContext>(options => 
// options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerConfigration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//EF SQL Migration and Data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try {
    var context = services.GetRequiredService<LMSDbContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedRoleData(context);
    await Seed.SeedRolePrivilegeData(context);
    await Seed.SeedUserData(context);
    await Seed.SeedLeaveType(context);
}
catch (Exception ex)
{ throw ex; }

app.Run();

