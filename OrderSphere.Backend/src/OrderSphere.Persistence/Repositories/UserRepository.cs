using Microsoft.EntityFrameworkCore;
using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Domain.Entities.Identity;
using OrderSphere.Persistence.Context;

namespace OrderSphere.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly OrderSphereDbContext _dbContext;
    
    public UserRepository(OrderSphereDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}