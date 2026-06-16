using MediatR;
using OrderSphere.Application.Common.Models;
using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Application.Interfaces.Security;
using OrderSphere.Domain.Entities.Identity;

namespace OrderSphere.Application.Features.Identity.Commands.LoginUser;

public sealed class LoginUserHandler 
    : IRequestHandler<LoginUserCommand, Result<LoginUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserSessionRepository _sessionRepository;

    public LoginUserHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider,
        IUserSessionRepository sessionRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _sessionRepository = sessionRepository;
    }

    public async Task<Result<LoginUserResponse>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            return Result<LoginUserResponse>.Failure("Invalid credentials");

        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            return Result<LoginUserResponse>.Failure("Invalid credentials");

        var accessToken = _jwtProvider.GenerateAccessToken(user);
        var refreshToken = _jwtProvider.GenerateRefreshToken();

        var session = new UserSession(
            user.Id,
            refreshToken,
            DateTime.UtcNow.AddDays(7),
            request.DeviceInfo ?? "unknown",
            request.IpAddress ?? "unknown"
        );

        await _sessionRepository.AddAsync(session, cancellationToken);

        user.UpdateLastLogin();
        await _userRepository.UpdateUserAsync(user, cancellationToken);

        return Result<LoginUserResponse>.Success(
            new LoginUserResponse(accessToken, refreshToken));
    }
}