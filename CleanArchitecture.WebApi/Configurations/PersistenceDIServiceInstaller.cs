using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Repositories;
using CleanArchitecture.Persistence.Services;
using CleanArchitecture.WebApi.Middlewares;
using GenericRepository;

namespace CleanArchitecture.WebApi.Configurations;

public sealed class PersistenceDIServiceInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddTransient<ExceptionMiddleware>();
        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

    }
}
