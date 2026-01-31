using Mooc.Application.Contracts.CourseChapter.Dto;

namespace Mooc.Application.Contracts.CourseChapter;

/// <summary>
/// Course Chapter Application Service Interface (Integrated CRUD Operations)
/// </summary>
public interface ICourseChapterAppService : ICrudService<CourseChapterOutputDto, CourseChapterOutputDto, long, FilterPagedResultRequestDto, CreateCourseChapterInputDto, UpdateCourseChapterInputDto>
{
    /// <summary>
    /// Get all chapters by course ID
    /// </summary>
    Task<List<CourseChapterOutputDto>> GetChaptersByCourseIdAsync(long courseId);

    /// <summary>
    /// Update chapter order
    /// </summary>
    Task UpdateChapterOrderAsync(long chapterId, int newOrder);

    /// <summary>
    /// Toggle chapter status (active/inactive)
    /// </summary>
    Task ToggleChapterStatusAsync(long chapterId);
}
