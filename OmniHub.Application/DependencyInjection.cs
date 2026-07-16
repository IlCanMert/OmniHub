using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OmniHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Bu projenin içindeki tüm Command ve Query'leri bulup sisteme kaydeder
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}