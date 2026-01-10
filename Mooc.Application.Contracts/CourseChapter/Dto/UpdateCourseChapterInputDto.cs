namespace Mooc.Application.Contracts.CourseChapter.Dto;

/// <summary>
/// 更新课程章节输入DTO
/// </summary>
public class UpdateCourseChapterInputDto : CreateOrUpdateCourseChapterBaseInputDto
{
    public long Id { get; set; }
}
