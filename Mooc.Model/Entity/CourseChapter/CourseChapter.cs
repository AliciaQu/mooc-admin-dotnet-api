namespace Mooc.Model.Entity.CourseChapter;

/// <summary>
/// 课程章节实体
/// </summary>
public class CourseChapter : BaseEntity
{
    /// <summary>
    /// 课程ID（外键，关联到课程模块）
    /// </summary>
    public long CourseId { get; set; }

    /// <summary>
    /// 章节名称
    /// </summary>
    public string ChapterName { get; set; }

    /// <summary>
    /// 章节描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 章节顺序
    /// </summary>
    public int OrderIndex { get; set; }

    /// <summary>
    /// 章节时长（分钟）
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// 是否免费试看
    /// </summary>
    public bool IsFree { get; set; } = false;

    /// <summary>
    /// 视频URL
    /// </summary>
    public string VideoUrl { get; set; }

    /// <summary>
    /// 资料URL
    /// </summary>
    public string MaterialUrl { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// 创建人ID
    /// </summary>
    public long CreatedBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// 更新人ID
    /// </summary>
    public long? UpdatedBy { get; set; }
}
