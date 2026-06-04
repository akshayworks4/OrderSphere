namespace OrderSphere.Domain.Entities.Identity;

public class User
{
    public Guid Id { get; private set; }
    public Guid RoleId { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string? PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    // Navigation Properties
    public Role Role { get; private set; } = default!;
    public ICollection<UserSession> UserSessions { get; private set; } = new List<UserSession>();
    
    private User()
    {
    }
    
    public User(
        Guid roleId,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        string? phoneNumber = null)
    {
        Id = Guid.NewGuid();
        RoleId = roleId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
    public void UpdateLastLogin()
    {
        LastLoginAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public void ChangePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
    }
}