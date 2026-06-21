namespace OrderSphere.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using OrderSphere.Application.Interfaces.Persistence;
using OrderSphere.Domain.Entities.Vendor;
using OrderSphere.Persistence.Context;

public class VendorApplicationRepository : IVendorApplicationRepository
{
    private readonly OrderSphereDbContext _context;

    public VendorApplicationRepository(OrderSphereDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(VendorApplication application, CancellationToken cancellationToken)
    {
        await _context.VendorApplications.AddAsync(application, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<VendorApplication?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.VendorApplications.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(VendorApplication application, CancellationToken cancellationToken)
    {
        _context.VendorApplications.Update(application);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsForUserAsync(Guid userId,CancellationToken cancellationToken)
    {
        return await _context.VendorApplications
            .AnyAsync(x => x.UserId == userId && x.Status == "Pending", cancellationToken);
    }
}