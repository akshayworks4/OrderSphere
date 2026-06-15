using MediatR;
using OrderSphere.Application.Common.Models;
using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Application.Interfaces.Security;
using OrderSphere.Domain.Entities.Identity;

namespace OrderSphere.Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Check if User Already exists
        if (await _userRepository.ExistsByEmailAsync(request.Email,cancellationToken))
        {
            return Result<Guid>.Failure("Email already exists");
        }
        
        // 2. Get default role (assumption: "Customer")
        var role = await _roleRepository.GetByNameAsync("Customer", cancellationToken);
        if (role is null)
            return Result<Guid>.Failure("Default role not found");
        
        // 3. Hash password
        var passwordHash = _passwordHasher.HashPassword(request.Password);
        
        // 4. Create user entity
        var user = new User(
            role.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash,
            request.PhoneNumber
        );

        // 5. Save to DB
        await _userRepository.AddUserAsync(user, cancellationToken);

        // 6. Return result
        return Result<Guid>.Success(user.Id);
    }
    
}