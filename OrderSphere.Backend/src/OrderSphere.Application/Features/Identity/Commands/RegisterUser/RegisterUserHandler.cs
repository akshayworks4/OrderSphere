// using MediatR;
// using OrderSphere.Application.Common.Models;
// using OrderSphere.Application.Interfaces.Persistence;
// using OrderSphere.Application.Interfaces.Security;
//
// namespace OrderSphere.Application.Features.Identity.Commands.RegisterUser;
//
// public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
// {
//     private readonly IUserRepository _userRepository;
//     private readonly IRoleRepository _roleRepository;
//     private readonly IPasswordHasher _passwordHasher;
//     public RegisterUserHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHasher passwordHasher)
//     {
//         _userRepository = userRepository;
//         _roleRepository = roleRepository;
//         _passwordHasher = passwordHasher;
//     }
//     public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
//     {
//            
//     }
//     
// }