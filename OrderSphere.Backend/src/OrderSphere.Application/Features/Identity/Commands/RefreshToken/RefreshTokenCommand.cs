using MediatR;
using OrderSphere.Application.Common.Models;
using OrderSphere.Application.Features.Identity.Commands.LoginUser;

namespace OrderSphere.Application.Features.Identity.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken)
    : IRequest<Result<LoginUserResponse>>;