using System.ComponentModel.DataAnnotations;
using Mooc.Shared.Entity.CourseChapter;

namespace Mooc.Application.Contracts.CourseChapter.Dto;

/// <summary>
/// Base Input DTO for Create or Update Course Chapter
/// </summary>
public class CreateOrUpdateCourseChapterBaseInputDto
{
    [Required(ErrorMessage = "Course ID is required")]
    public long CourseId { get; set; }

    [Required(ErrorMessage = "Chapter name is required")]
    [MaxLength(CourseChapterEntityConsts.MaxChapterNameLength, ErrorMessage = "Chapter name cannot exceed {1} characters")]
    public string ChapterName { get; set; }

    [MaxLength(CourseChapterEntityConsts.MaxDescriptionLength, ErrorMessage = "Description cannot exceed {1} characters")]
    public string Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Order index must be greater than 0")]
    public int OrderIndex { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Duration cannot be negative")]
    public int Duration { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsFree { get; set; } = false;

    [MaxLength(CourseChapterEntityConsts.MaxVideoUrlLength)]
    public string VideoUrl { get; set; }

    [MaxLength(CourseChapterEntityConsts.MaxMaterialUrlLength)]
    public string MaterialUrl { get; set; }
}
