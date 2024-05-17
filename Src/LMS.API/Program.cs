using LMS.Infrastructure.Repositories;
using LMS.Infrastructure.Database;
using LMS.Application.Interfaces;
using LMS.Application.Services;
using LMS.Application.Mappings;
using LMS.Application.IServiceMappings;

using Microsoft.EntityFrameworkCore;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
    policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<LMSDbContext>(options => 
// options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddDbContext<LMSDbContext>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

//AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Generic Services
builder.Services.AddScoped(typeof(IReadServiceAsync<,>), typeof(ReadServiceAsync<,>));

// Services
builder.Services.AddScoped(typeof(IUserMapping), typeof(UserMapping));
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

app.MapControllers();

app.Run();

