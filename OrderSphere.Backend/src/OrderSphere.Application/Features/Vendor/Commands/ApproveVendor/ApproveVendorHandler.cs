using MediatR;
using OrderSphere.Application.Common.Models;
using OrderSphere.Application.Interfaces.Persistence;

namespace OrderSphere.Application.Features.Vendor.Commands.ApproveVendor;

public sealed class ApproveVendorHandler : IRequestHandler<ApproveVendorCommand, Result>
{
    private readonly IVendorApplicationRepository _vendorRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public ApproveVendorHandler(
        IVendorApplicationRepository vendorRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _vendorRepository = vendorRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Handle(ApproveVendorCommand request, CancellationToken cancellationToken)
    {
        var application = await _vendorRepository.GetByIdAsync(request.ApplicationId, cancellationToken);
        if (application is null)
        {
            return Result.Failure("Application not found.");
        }

        var user = await _userRepository.GetByIdAsync(application.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure("User not found.");
        }

        var vendorRole = await _roleRepository.GetByNameAsync("Vendor", cancellationToken);
        if (vendorRole is null)
        {
            return Result.Failure("Vendor role not found.");
        }

        application.Approve();
        user.ChangeRole(vendorRole.Id);

        await _vendorRepository.UpdateAsync(application, cancellationToken);
        await _userRepository.UpdateUserAsync(user, cancellationToken);

        return Result.Success();
    }
}