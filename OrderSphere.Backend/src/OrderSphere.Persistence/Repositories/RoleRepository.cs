using Microsoft.EntityFrameworkCore;
using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Domain.Entities.Identity;
using OrderSphere.Persistence.Context;

namespace OrderSphere.Persistence.Repositories;

public class RoleRepository  : IRoleRepository
{
    private readonly OrderSphereDbContext _dbContext;

    public RoleRepository(OrderSphereDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}