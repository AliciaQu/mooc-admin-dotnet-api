using System.ComponentModel.DataAnnotations;
using Mooc.Shared.Entity.CourseChapter;

namespace Mooc.Application.Contracts.CourseChapter.Dto;

/// <summary>
/// 创建或更新课程章节基础输入DTO
/// </summary>
public class CreateOrUpdateCourseChapterBaseInputDto
{
    [Required(ErrorMessage = "课程ID不能为空")]
    public long CourseId { get; set; }

    [Required(ErrorMessage = "章节名称不能为空")]
    [MaxLength(CourseChapterEntityConsts.MaxChapterNameLength, ErrorMessage = "章节名称长度不能超过{1}个字符")]
    public string ChapterName { get; set; }

    [MaxLength(CourseChapterEntityConsts.MaxDescriptionLength, ErrorMessage = "描述长度不能超过{1}个字符")]
    public string Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "章节顺序必须大于0")]
    public int OrderIndex { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "时长不能为负数")]
    public int Duration { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsFree { get; set; } = false;

    [MaxLength(CourseChapterEntityConsts.MaxVideoUrlLength)]
    public string VideoUrl { get; set; }

    [MaxLength(CourseChapterEntityConsts.MaxMaterialUrlLength)]
    public string MaterialUrl { get; set; }
}
