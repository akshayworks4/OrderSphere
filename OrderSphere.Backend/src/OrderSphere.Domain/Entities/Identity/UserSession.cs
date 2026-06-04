namespace OrderSphere.Domain.Entities.Identity;

public class UserSession
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string RefreshToken { get; private set; } = default!;
    public string? DeviceInfo { get; private set; }
    public string? IpAddress { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    // Navigation Property
    public User User { get; private set; } = default!;
    
    private UserSession()
    {
    }

    public UserSession(
        Guid userId,
        string refreshToken,
        DateTime expiresAt,
        string? deviceInfo = null,
        string? ipAddress = null)
    {
        Id = Guid.NewGuid();

        UserId = userId;

        RefreshToken = refreshToken;

        DeviceInfo = deviceInfo;
        IpAddress = ipAddress;

        ExpiresAt = expiresAt;

        IsActive = true;
        
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Revoke()
    {
        IsActive = false;
        RevokedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}