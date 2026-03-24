namespace Mooc.Model.Entity.Course;

/// <summary>
/// Course Status Enum
/// </summary>
public enum CourseStatus
{
    /// <summary>
    /// Draft status
    /// </summary>
    Draft = 1,
    
    /// <summary>
    /// Published status
    /// </summary>
    Published = 2,
    
    /// <summary>
    /// Archived status
    /// </summary>
    Archived = 3
}

/// <summary>
/// Course Entity
/// </summary>
public class Course : BaseEntity
{
    /// <summary>
    /// Course Title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Course Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Cover Image URL
    /// </summary>
    public string CoverImage { get; set; }

    /// <summary>
    /// Category ID (Foreign Key)
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Teacher ID (Foreign Key)
    /// </summary>
    public long TeacherId { get; set; }

    /// <summary>
    /// Course Status
    /// </summary>
    public CourseStatus Status { get; set; } = CourseStatus.Draft;

    /// <summary>
    /// Is Active/Enabled
    /// </summary>
    public bool IsActive { get; set; } = true;

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
    /// One course belongs to one category
    /// </summary>
    public virtual Category.Category Category { get; set; }

    /// <summary>
    /// One course belongs to one teacher (user)
    /// </summary>
    public virtual User Teacher { get; set; }

    /// <summary>
    /// One course has many chapters
    /// </summary>
    public virtual ICollection<CourseChapter.CourseChapter> Chapters { get; set; }
}
