using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Repositories;
using CleanArchitecture.Persistence.Services;
using CleanArchitecture.WebApi.Middlewares;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistence.AssemblyReference).Assembly);

string connectionString = builder.Configuration.GetConnectionString("SqlServerConnection")!;
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);

builder.Services.AddMediatR(cfr =>
    cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(sc => sc.Layout = ScalarLayout.Classic);
}

app.UseMiddlewareExtensions();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
