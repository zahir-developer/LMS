
using LMS.Application.ServiceMappings;
using LMS.Application.IServiceMappings;
using LMS.Application.Interfaces.IServices;


using System.Text;
using Microsoft.OpenApi.Models;
using LMS.API.Extensions;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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

app.Run();

