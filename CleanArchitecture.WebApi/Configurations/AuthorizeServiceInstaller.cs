namespace CleanArchitecture.WebApi.Configurations;

public sealed class AuthorizeServiceInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {

        services.AddAuthentication().AddJwtBearer();
        services.AddAuthorization();
    }
}
