namespace OrderSphere.Application.Interfaces.Persistence;

using OrderSphere.Domain.Entities.Vendor;

public interface IVendorApplicationRepository
{
    Task AddAsync(
        VendorApplication application,
        CancellationToken cancellationToken);

    Task<VendorApplication?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task UpdateAsync(
        VendorApplication application,
        CancellationToken cancellationToken);

    Task<bool> ExistsForUserAsync(
        Guid userId,
        CancellationToken cancellationToken);
}