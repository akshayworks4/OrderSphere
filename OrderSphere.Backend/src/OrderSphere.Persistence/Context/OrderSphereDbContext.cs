using Microsoft.EntityFrameworkCore;
using OrderSphere.Domain.Entities.Identity;

namespace OrderSphere.Persistence.Context;

public class OrderSphereDbContext(DbContextOptions<OrderSphereDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(OrderSphereDbContext).Assembly);
    }
}