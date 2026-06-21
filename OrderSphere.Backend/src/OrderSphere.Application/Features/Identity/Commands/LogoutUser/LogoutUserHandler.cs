using MediatR;
using OrderSphere.Application.Common.Models;
using OrderSphere.Application.Interfaces.Persistence;

namespace OrderSphere.Application.Features.Identity.Commands.LogoutUser;

public sealed class LogoutUserHandler : IRequestHandler<LogoutUserCommand, Result>
{
    private readonly IUserSessionRepository _sessionRepository;

    public LogoutUserHandler(IUserSessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<Result> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (session is null)
            return Result.Failure("Session not found");

        session.Revoke();   

        await _sessionRepository.UpdateAsync(session, cancellationToken);

        return Result.Success();
    }
}