using MediatR;
using OrderSphere.Application.Common.Models;
using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Application.Interfaces.Security;
using OrderSphere.Domain.Entities.Vendor;

namespace OrderSphere.Application.Features.Vendor.Commands.ApplyVendor;

public class ApplyVendorHandler : IRequestHandler<ApplyVendorCommand, Result<Guid>>
{
    private readonly IVendorApplicationRepository _repository;
    private readonly ICurrentUser _currentUser;
    
    public ApplyVendorHandler(IVendorApplicationRepository repository, ICurrentUser currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<Result<Guid>> Handle(ApplyVendorCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var alreadyApplied = await _repository.ExistsForUserAsync(userId, cancellationToken);

        if (alreadyApplied)
            return Result<Guid>.Failure("Already applied");

        var application = new VendorApplication(
            userId,
            request.BusinessName,
            request.Description
            );

        await _repository.AddAsync(application, cancellationToken);

        return Result<Guid>.Success(application.Id);
    }
}