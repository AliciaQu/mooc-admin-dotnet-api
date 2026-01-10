using System.ComponentModel.DataAnnotations;
using Mooc.Shared.Entity.CourseChapter;

namespace Mooc.Model.Entity.CourseChapter;

/// <summary>
/// 课程章节实体
/// </summary>
public class CourseChapter : BaseEntity
{
    /// <summary>
    /// 课程ID
    /// </summary>
    [Required]
    public long CourseId { get; set; }

    /// <summary>
    /// 章节名称
    /// </summary>
    [Required]
    [MaxLength(CourseChapterEntityConsts.MaxChapterNameLength)]
    public string ChapterName { get; set; }

    /// <summary>
    /// 章节描述
    /// </summary>
    [MaxLength(CourseChapterEntityConsts.MaxDescriptionLength)]
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
    [MaxLength(CourseChapterEntityConsts.MaxVideoUrlLength)]
    public string VideoUrl { get; set; }

    /// <summary>
    /// 资料URL
    /// </summary>
    [MaxLength(CourseChapterEntityConsts.MaxMaterialUrlLength)]
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
