namespace Mooc.Model.Entity.Course;

/// <summary>
/// 课程实体（示例 - 由Terence负责完善）
/// </summary>
public class Course : BaseEntity
{
    /// <summary>
    /// 课程名称
    /// </summary>
    public string CourseName { get; set; }

    /// <summary>
    /// 课程描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    // 导航属性：一个课程包含多个章节
    public virtual ICollection<CourseChapter.CourseChapter> Chapters { get; set; }
}
