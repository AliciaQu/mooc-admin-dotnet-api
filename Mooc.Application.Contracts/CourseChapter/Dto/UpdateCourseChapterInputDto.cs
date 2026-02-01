namespace Mooc.Application.Contracts.CourseChapter.Dto;

/// <summary>
/// Input DTO for Updating Course Chapter
/// </summary>
public class UpdateCourseChapterInputDto : CreateOrUpdateCourseChapterBaseInputDto
{
    public long Id { get; set; }
}
