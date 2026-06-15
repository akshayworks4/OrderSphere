using Microsoft.Extensions.DependencyInjection;
using OrderSphere.Application.Interfaces.Security;
using OrderSphere.Infrastructure.Security;

namespace OrderSphere.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Security
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}