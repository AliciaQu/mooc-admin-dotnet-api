namespace Mooc.Application.Contracts.CourseChapter;

/// <summary>
/// 课程章节读取服务接口
/// </summary>
public interface ICourseChapterReadService : IReadOnlyService<CourseChapterOutputDto, CourseChapterOutputDto, long, FilterPagedResultRequestDto>
{
    /// <summary>
    /// 根据课程ID获取所有章节
    /// </summary>
    Task<List<CourseChapterOutputDto>> GetChaptersByCourseIdAsync(long courseId);
}
