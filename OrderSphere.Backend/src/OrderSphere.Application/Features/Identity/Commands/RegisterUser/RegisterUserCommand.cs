using MediatR;
using OrderSphere.Application.Common.Models;

namespace OrderSphere.Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<Result<Guid>>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? PhoneNumber { get; set; }
}