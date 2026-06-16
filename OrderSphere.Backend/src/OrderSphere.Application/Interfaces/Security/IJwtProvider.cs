using OrderSphere.Domain.Entities.Identity;

namespace OrderSphere.Application.Interfaces.Security;

public interface IJwtProvider
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();
}