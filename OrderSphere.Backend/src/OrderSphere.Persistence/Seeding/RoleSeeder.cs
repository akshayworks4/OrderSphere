using Microsoft.EntityFrameworkCore;
using OrderSphere.Domain.Entities.Identity;
using OrderSphere.Persistence.Context;

namespace OrderSphere.Persistence.Seeding;

public static class RoleSeeder
{
    public static async Task SeedAsync(OrderSphereDbContext dbContext)
    {
        if (await dbContext.Roles.AnyAsync()) return;

        var roles = new List<Role>
        {
            new Role("Admin"),
            new Role("Customer"),
            new Role("Vendor")
        };

        await dbContext.Roles.AddRangeAsync(roles);
        await dbContext.SaveChangesAsync();
    }
}