using MediatR;
using OrderSphere.Application.Common.Models;
using OrderSphere.Application.Features.Identity.Commands.LoginUser;
using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Application.Interfaces.Security;
using OrderSphere.Domain.Entities.Identity;

namespace OrderSphere.Application.Features.Identity.Commands.RefreshToken;

public sealed class RefreshTokenHandler
    : IRequestHandler<RefreshTokenCommand, Result<LoginUserResponse>>
{
    private readonly IUserSessionRepository _sessionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public RefreshTokenHandler(
        IUserSessionRepository sessionRepository,
        IUserRepository userRepository,
        IJwtProvider jwtProvider)
    {
        _sessionRepository = sessionRepository;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginUserResponse>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var session = await _sessionRepository
            .GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (session is null || !session.IsActive || session.RevokedAt!= null)
            return Result<LoginUserResponse>.Failure("Invalid refresh token");

        if (session.ExpiresAt < DateTime.UtcNow)
            return Result<LoginUserResponse>.Failure("Refresh token expired");

        var user = await _userRepository.GetByIdAsync(session.UserId, cancellationToken);

        if (user is null)
            return Result<LoginUserResponse>.Failure("User not found");

        // Generate new tokens (ROTATION)
        var newAccessToken = _jwtProvider.GenerateAccessToken(user);
        var newRefreshToken = _jwtProvider.GenerateRefreshToken();

        // Update session
        session.Revoke(); // mark old token inactive
        await _sessionRepository.UpdateAsync(session, cancellationToken);

        var newSession = new UserSession(
            user.Id,
            newRefreshToken,
            DateTime.UtcNow.AddDays(7),
            session.DeviceInfo,
            session.IpAddress
        );

        await _sessionRepository.AddAsync(newSession, cancellationToken);

        return Result<LoginUserResponse>.Success(
            new LoginUserResponse(newAccessToken, newRefreshToken));
    }
}