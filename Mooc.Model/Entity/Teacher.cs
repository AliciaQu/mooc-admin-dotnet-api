namespace Mooc.Model.Entity;

/// <summary>
/// Teacher Entity
/// </summary>
public class Teacher : BaseEntity
{
    /// <summary>
    /// User ID (Foreign Key)
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Expertise
    /// </summary>
    public string Expertise { get; set; }

    // Navigation property
    /// <summary>
    /// User
    /// </summary>
    public virtual User User { get; set; }
}
