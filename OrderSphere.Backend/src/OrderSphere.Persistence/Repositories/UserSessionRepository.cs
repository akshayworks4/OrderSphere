using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Domain.Entities.Identity;
using OrderSphere.Persistence.Context;

namespace OrderSphere.Persistence.Repositories;

public class UserSessionRepository : IUserSessionRepository
{
    private readonly OrderSphereDbContext _dbContext;
    public UserSessionRepository(OrderSphereDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(UserSession userSession, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserSession> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(UserSession userSession, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}