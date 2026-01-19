namespace Mooc.Application.CourseChapter;

/// <summary>
/// 课程章节应用服务实现（整合CRUD操作）
/// </summary>
public class CourseChapterAppService : CrudService<Model.Entity.CourseChapter.CourseChapter, CourseChapterOutputDto, CourseChapterOutputDto, long, FilterPagedResultRequestDto, CreateCourseChapterInputDto, UpdateCourseChapterInputDto>, 
    ICourseChapterAppService, 
    ITransientDependency
{
    public CourseChapterAppService()
    {
    }

    /// <summary>
    /// 创建过滤查询
    /// </summary>
    protected override IQueryable<Model.Entity.CourseChapter.CourseChapter> CreateFilteredQuery(FilterPagedResultRequestDto input)
    {
        var query = base.CreateFilteredQuery(input);

        // 支持按章节名称或描述搜索
        if (input != null && !string.IsNullOrWhiteSpace(input.Filter))
        {
            query = query.Where(x => x.ChapterName.Contains(input.Filter) || 
                                    (x.Description != null && x.Description.Contains(input.Filter)));
        }

        // 默认排序：先按课程ID，再按章节顺序
        return query.OrderBy(x => x.CourseId).ThenBy(x => x.OrderIndex);
    }

    /// <summary>
    /// 创建实体前的处理
    /// </summary>
    protected override Model.Entity.CourseChapter.CourseChapter MapToEntity(CreateCourseChapterInputDto createInput)
    {
        var entity = base.MapToEntity(createInput);
        
        // 设置创建时间和创建人
        entity.CreatedAt = DateTime.Now;
        // TODO: 从当前用户上下文获取用户ID
        entity.CreatedBy = 1; 
        
        return entity;
    }

    /// <summary>
    /// 更新实体前的处理
    /// </summary>
    protected override void MapToEntity(UpdateCourseChapterInputDto updateInput, Model.Entity.CourseChapter.CourseChapter entity)
    {
        base.MapToEntity(updateInput, entity);
        
        // 设置更新时间和更新人
        entity.UpdatedAt = DateTime.Now;
        // TODO: 从当前用户上下文获取用户ID
        entity.UpdatedBy = 1;
    }

    /// <summary>
    /// 根据课程ID获取所有章节
    /// </summary>
    public async Task<List<CourseChapterOutputDto>> GetChaptersByCourseIdAsync(long courseId)
    {
        var queryable = await Repository.GetQueryableAsync();
        var chapters = queryable
            .Where(x => x.CourseId == courseId)
            .OrderBy(x => x.OrderIndex)
            .ToList();
        
        return ObjectMapper.Map<List<Model.Entity.CourseChapter.CourseChapter>, List<CourseChapterOutputDto>>(chapters);
    }

    /// <summary>
    /// 更新章节顺序
    /// </summary>
    public async Task UpdateChapterOrderAsync(long chapterId, int newOrder)
    {
        var chapter = await GetEntityByIdAsync(chapterId);
        if (chapter == null)
        {
            throw new Exception($"章节不存在: {chapterId}");
        }

        chapter.OrderIndex = newOrder;
        chapter.UpdatedAt = DateTime.Now;
        // TODO: 从当前用户上下文获取用户ID
        chapter.UpdatedBy = 1;

        var dbSet = GetDbSet();
        if (dbSet.Local.All(e => e != chapter))
        {
            dbSet.Attach(chapter);
            dbSet.Update(chapter);
        }
        await McDBContext.SaveChangesAsync();
    }

    /// <summary>
    /// 切换章节状态（启用/禁用）
    /// </summary>
    public async Task ToggleChapterStatusAsync(long chapterId)
    {
        var chapter = await GetEntityByIdAsync(chapterId);
        if (chapter == null)
        {
            throw new Exception($"章节不存在: {chapterId}");
        }

        chapter.IsActive = !chapter.IsActive;
        chapter.UpdatedAt = DateTime.Now;
        // TODO: 从当前用户上下文获取用户ID
        chapter.UpdatedBy = 1;

        var dbSet = GetDbSet();
        if (dbSet.Local.All(e => e != chapter))
        {
            dbSet.Attach(chapter);
            dbSet.Update(chapter);
        }
        await McDBContext.SaveChangesAsync();
    }
}
