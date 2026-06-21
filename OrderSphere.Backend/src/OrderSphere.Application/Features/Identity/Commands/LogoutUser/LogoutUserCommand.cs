using MediatR;
using OrderSphere.Application.Common.Models;

namespace OrderSphere.Application.Features.Identity.Commands.LogoutUser;

public sealed record LogoutUserCommand(string RefreshToken) : IRequest<Result>;