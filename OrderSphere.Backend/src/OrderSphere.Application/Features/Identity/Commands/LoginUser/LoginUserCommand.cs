using MediatR;
using OrderSphere.Application.Common.Models;
using OrderSphere.Application.Features.Identity.Commands.LoginUser;

public sealed record LoginUserCommand(
    string Email,
    string Password,
    string? DeviceInfo,
    string? IpAddress)
    : IRequest<Result<LoginUserResponse>>;