namespace Mooc.Model.Entity;

/// <summary>
/// User Role Entity (Join Table for many-to-many relationship)
/// </summary>
public class UserRole : BaseEntity
{
    /// <summary>
    /// User ID (Foreign Key)
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Role ID (Foreign Key)
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// Created At
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    /// <summary>
    /// User
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Role
    /// </summary>
    public virtual Role Role { get; set; }
}
