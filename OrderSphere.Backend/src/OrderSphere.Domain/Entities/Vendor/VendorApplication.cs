using OrderSphere.Domain.Entities.Identity;

namespace OrderSphere.Domain.Entities.Vendor;

public class VendorApplication
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string BusinessName { get; private set; } = default!;
    public string? Description { get; private set; }
    public string Status { get; private set; } = "Pending"; // Pending, Approved, Rejected
    public DateTime CreatedAt { get; private set; }
    public DateTime? ReviewedAt { get; private set; }

    public User User { get; private set; } = default!;
    private VendorApplication() { }
    public VendorApplication(Guid userId, string businessName, string? description)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        BusinessName = businessName;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }
    public void Approve()
    {
        Status = "Approved";
        ReviewedAt = DateTime.UtcNow;
    }
    public void Reject()
    {
        Status = "Rejected";
        ReviewedAt = DateTime.UtcNow;
    }
}