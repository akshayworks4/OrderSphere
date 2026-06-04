namespace OrderSphere.Domain.Entities.Identity;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    // Navigation Properties
    public ICollection<User> Users { get; private set; } = new List<User>();
    
    private Role()
    {
    }

    public Role(
        string name,
        string? description = null)
    {
        Id = Guid.NewGuid();

        Name = name;
        Description = description;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}