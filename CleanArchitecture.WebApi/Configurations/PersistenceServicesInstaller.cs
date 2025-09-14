using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchitecture.WebApi.Configurations;

public class PersistenceServicesInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddAutoMapper(typeof(Persistence.AssemblyReference).Assembly);

        string connectionString = configuration.GetConnectionString("SqlServerConnection")!;
        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.MSSqlServer(connectionString, tableName: "Logs", autoCreateSqlTable: true)
            .CreateLogger();

        host.UseSerilog();
    }
}
