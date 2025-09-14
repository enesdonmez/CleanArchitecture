﻿using System.Reflection;

namespace CleanArchitecture.WebApi.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration, IHostBuilder host, params Assembly[] assemblies)
    {
        IEnumerable<IServiceInstaller> serviceInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssaignableToType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

        foreach (IServiceInstaller installer in serviceInstallers)
        {
            installer.InstallServices(services, configuration, host);
        }
        return services;

        static bool IsAssaignableToType<T>(TypeInfo typeInfo) =>
            typeof(T).IsAssignableFrom(typeInfo) && !typeInfo.IsInterface && !typeInfo.IsAbstract;

    }
}
