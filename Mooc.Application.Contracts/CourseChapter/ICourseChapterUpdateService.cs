namespace Mooc.Application.Contracts.CourseChapter;

/// <summary>
/// 课程章节更新服务接口
/// </summary>
public interface ICourseChapterUpdateService : IUpdateService<UpdateCourseChapterInputDto, CourseChapterOutputDto, long>
{
    /// <summary>
    /// 更新章节顺序
    /// </summary>
    Task UpdateChapterOrderAsync(long chapterId, int newOrder);

    /// <summary>
    /// 切换章节状态（启用/禁用）
    /// </summary>
    Task ToggleChapterStatusAsync(long chapterId);
}
