namespace Mooc.Model.Entity.CourseChapter;

/// <summary>
/// Course Chapter Entity
/// </summary>
public class CourseChapter : BaseEntity
{
    /// <summary>
    /// Course ID (Foreign Key)
    /// </summary>
    public long CourseId { get; set; }

    /// <summary>
    /// Chapter Name
    /// </summary>
    public string ChapterName { get; set; }

    /// <summary>
    /// Chapter Description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Chapter Order Index
    /// </summary>
    public int OrderIndex { get; set; }

    /// <summary>
    /// Chapter Duration (in minutes)
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Is Active/Enabled
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Is Free Preview
    /// </summary>
    public bool IsFree { get; set; } = false;

    /// <summary>
    /// Video URL
    /// </summary>
    public string VideoUrl { get; set; }

    /// <summary>
    /// Material/Resource URL
    /// </summary>
    public string MaterialUrl { get; set; }

    /// <summary>
    /// Created At
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Created By User ID
    /// </summary>
    public long CreatedBy { get; set; }

    /// <summary>
    /// Updated At
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Updated By User ID
    /// </summary>
    public long? UpdatedBy { get; set; }

    // Navigation property: many chapters belong to one course
    public virtual Course.Course Course { get; set; }
}
