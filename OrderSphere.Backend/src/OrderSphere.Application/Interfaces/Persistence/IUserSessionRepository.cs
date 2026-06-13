using OrderSphere.Domain.Entities.Identity;

namespace OrderSphere.Application.Interfaces.Persistence;

public interface IUserSessionRepository
{
    Task AddAsync(UserSession userSession, CancellationToken cancellationToken = default);
    Task<UserSession> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserSession userSession, CancellationToken cancellationToken = default);
}