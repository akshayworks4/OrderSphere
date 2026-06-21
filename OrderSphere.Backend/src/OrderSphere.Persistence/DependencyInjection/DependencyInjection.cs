using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Domain.Entities.Identity;
using OrderSphere.Persistence.Context;
using OrderSphere.Persistence.Repositories;

namespace OrderSphere.Persistence.DependencyInjection;

public static class DependencyInjection 
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderSphereDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
        });

        // Add Repository Services
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserSessionRepository, UserSessionRepository>();
        services.AddScoped<IVendorApplicationRepository, VendorApplicationRepository>();

        return services;
    }
}