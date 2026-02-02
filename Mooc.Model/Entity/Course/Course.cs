namespace Mooc.Model.Entity.Course;

/// <summary>
/// Course Entity (Example - To be completed by Terence)
/// </summary>
public class Course : BaseEntity
{
    /// <summary>
    /// Course Name
    /// </summary>
    public string CourseName { get; set; }

    /// <summary>
    /// Course Description
    /// </summary>
    public string Description { get; set; }

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

    // Navigation property: one course has many chapters
    public virtual ICollection<CourseChapter.CourseChapter> Chapters { get; set; }
}
