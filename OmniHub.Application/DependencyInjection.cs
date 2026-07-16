using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OmniHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Finds all commands and queries in this project and registers them
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}