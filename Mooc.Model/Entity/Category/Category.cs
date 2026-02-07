namespace Mooc.Model.Entity.Category;

/// <summary>
/// Category Entity
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Category Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Category Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Parent Category ID (Self-referencing)
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// Sort Order
    /// </summary>
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// Is Active/Enabled
    /// </summary>
    public bool Active { get; set; } = true;

    /// <summary>
    /// Created At
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Updated At
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    /// <summary>
    /// One category belongs to one parent category (self-referencing)
    /// </summary>
    public virtual Category Parent { get; set; }

    /// <summary>
    /// One category has many child categories (self-referencing)
    /// </summary>
    public virtual ICollection<Category> Children { get; set; }

    /// <summary>
    /// One category has many courses
    /// </summary>
    public virtual ICollection<Course.Course> Courses { get; set; }
}