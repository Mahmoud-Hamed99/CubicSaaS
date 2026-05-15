using AutoMapper;
using Cubic.Api.Middleware;
using Cubic.Application.Dtos;
using Cubic.Application.Implmentations;
using Cubic.Application.Interfaces;
using Cubic.Application.Jobs;
using Cubic.Application.MappingProfile;
using Cubic.Application.Validators;
using Cubic.Core.Entities;
using Cubic.Core.Interfaces;
using Cubic.Infrastructure.Context;
using Cubic.Infrastructure.Implmentations;
using FluentValidation;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CubicDB")));

builder.Services.AddScoped<ITenantContext, TenantContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IRepository<Tenant>, TenantRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();


builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IUserService, UserService>();

//FluentValidation
builder.Services.AddScoped<IValidator<TenantDto>, TenantValidator>();
builder.Services.AddScoped<IValidator<UserDto>, UserValidator>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<UserJobs>();

builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("CubicDB")
    ));

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseMiddleware<TenantResolutionMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseRouting();

app.UseHangfireDashboard("/hangfire");


RecurringJob.AddOrUpdate<UserJobs>(
    "log-active-users-per-tenant",
    job => job.LogActiveUsersPerTenantAsync(),
    Cron.Minutely);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
