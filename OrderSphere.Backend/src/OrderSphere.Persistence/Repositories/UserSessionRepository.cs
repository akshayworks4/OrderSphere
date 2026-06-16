using Microsoft.EntityFrameworkCore;
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

    public async Task AddAsync(
        UserSession userSession,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.UserSessions.AddAsync(userSession, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserSession?> GetByRefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserSessions
            .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken);
    }

    public async Task UpdateAsync(
        UserSession userSession,
        CancellationToken cancellationToken = default)
    {
        _dbContext.UserSessions.Update(userSession);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}