using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.WebApi.Configurations;

public class PersistenceServicesInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(CleanArchitecture.Persistence.AssemblyReference).Assembly);

        string connectionString = configuration.GetConnectionString("SqlServerConnection")!;
        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>();
    }
}
