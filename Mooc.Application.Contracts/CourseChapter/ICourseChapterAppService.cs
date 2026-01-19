namespace Mooc.Application.Contracts.CourseChapter;

/// <summary>
/// 课程章节应用服务接口（整合CRUD操作）
/// </summary>
public interface ICourseChapterAppService : ICrudService<CourseChapterOutputDto, CourseChapterOutputDto, long, FilterPagedResultRequestDto, CreateCourseChapterInputDto, UpdateCourseChapterInputDto>
{
    /// <summary>
    /// 根据课程ID获取所有章节
    /// </summary>
    Task<List<CourseChapterOutputDto>> GetChaptersByCourseIdAsync(long courseId);

    /// <summary>
    /// 更新章节顺序
    /// </summary>
    Task UpdateChapterOrderAsync(long chapterId, int newOrder);

    /// <summary>
    /// 切换章节状态（启用/禁用）
    /// </summary>
    Task ToggleChapterStatusAsync(long chapterId);
}
