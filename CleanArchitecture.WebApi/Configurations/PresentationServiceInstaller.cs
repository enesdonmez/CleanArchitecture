using CleanArchitecture.Application.Extensions;

namespace CleanArchitecture.WebApi.Configurations;

public sealed class PresentationServiceInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .SetIsOriginAllowed(policy => true);
            });
        });

        services.AddControllers()
            .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);


        services.AddOpenApi(opt =>
        {
            opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
    }
}
