namespace Mooc.Application.CourseChapter;

/// <summary>
/// 课程章节更新服务实现
/// </summary>
public class CourseChapterUpdateService : UpdateService<Model.Entity.CourseChapter.CourseChapter, UpdateCourseChapterInputDto, CourseChapterOutputDto, long>, ICourseChapterUpdateService, ITransientDependency
{
    public CourseChapterUpdateService()
    {
    }

    protected override async Task MapToEntityAsync(UpdateCourseChapterInputDto updateInput, Model.Entity.CourseChapter.CourseChapter entity)
    {
        await base.MapToEntityAsync(updateInput, entity);
        
        // 设置更新时间和更新人
        entity.UpdatedAt = DateTime.Now;
        // TODO: 从当前用户上下文获取用户ID
        entity.UpdatedBy = 1;
    }

    /// <summary>
    /// 更新章节顺序
    /// </summary>
    public async Task UpdateChapterOrderAsync(long chapterId, int newOrder)
    {
        var chapter = await Repository.GetAsync(chapterId);
        if (chapter == null)
        {
            throw new Exception($"章节不存在: {chapterId}");
        }

        chapter.OrderIndex = newOrder;
        chapter.UpdatedAt = DateTime.Now;
        // TODO: 从当前用户上下文获取用户ID
        chapter.UpdatedBy = 1;

        await Repository.UpdateAsync(chapter);
    }

    /// <summary>
    /// 切换章节状态（启用/禁用）
    /// </summary>
    public async Task ToggleChapterStatusAsync(long chapterId)
    {
        var chapter = await Repository.GetAsync(chapterId);
        if (chapter == null)
        {
            throw new Exception($"章节不存在: {chapterId}");
        }

        chapter.IsActive = !chapter.IsActive;
        chapter.UpdatedAt = DateTime.Now;
        // TODO: 从当前用户上下文获取用户ID
        chapter.UpdatedBy = 1;

        await Repository.UpdateAsync(chapter);
    }
}
