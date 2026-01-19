namespace Mooc.Application.CourseChapter;

/// <summary>
/// Course Chapter Application Service Implementation (Integrated CRUD Operations)
/// </summary>
public class CourseChapterAppService : CrudService<Model.Entity.CourseChapter.CourseChapter, CourseChapterOutputDto, CourseChapterOutputDto, long, FilterPagedResultRequestDto, CreateCourseChapterInputDto, UpdateCourseChapterInputDto>, 
    ICourseChapterAppService, 
    ITransientDependency
{
    public CourseChapterAppService()
    {
    }

    /// <summary>
    /// Create filtered query
    /// </summary>
    protected override IQueryable<Model.Entity.CourseChapter.CourseChapter> CreateFilteredQuery(FilterPagedResultRequestDto input)
    {
        var query = base.CreateFilteredQuery(input);

        // Support search by chapter name or description
        if (input != null && !string.IsNullOrWhiteSpace(input.Filter))
        {
            query = query.Where(x => x.ChapterName.Contains(input.Filter) || 
                                    (x.Description != null && x.Description.Contains(input.Filter)));
        }

        // Default sorting: by course ID, then by order index
        return query.OrderBy(x => x.CourseId).ThenBy(x => x.OrderIndex);
    }

    /// <summary>
    /// Processing before creating entity
    /// </summary>
    protected override Model.Entity.CourseChapter.CourseChapter MapToEntity(CreateCourseChapterInputDto createInput)
    {
        var entity = base.MapToEntity(createInput);
        
        // Set created timestamp and creator
        entity.CreatedAt = DateTime.Now;
        // TODO: Get user ID from current user context
        entity.CreatedBy = 1; 
        
        return entity;
    }

    /// <summary>
    /// Processing before updating entity
    /// </summary>
    protected override void MapToEntity(UpdateCourseChapterInputDto updateInput, Model.Entity.CourseChapter.CourseChapter entity)
    {
        base.MapToEntity(updateInput, entity);
        
        // Set updated timestamp and updater
        entity.UpdatedAt = DateTime.Now;
        // TODO: Get user ID from current user context
        entity.UpdatedBy = 1;
    }

    /// <summary>
    /// Get all chapters by course ID
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
    /// Update chapter order
    /// </summary>
    public async Task UpdateChapterOrderAsync(long chapterId, int newOrder)
    {
        var chapter = await GetEntityByIdAsync(chapterId);
        if (chapter == null)
        {
            throw new Exception($"Chapter not found: {chapterId}");
        }

        chapter.OrderIndex = newOrder;
        chapter.UpdatedAt = DateTime.Now;
        // TODO: Get user ID from current user context
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
    /// Toggle chapter status (active/inactive)
    /// </summary>
    public async Task ToggleChapterStatusAsync(long chapterId)
    {
        var chapter = await GetEntityByIdAsync(chapterId);
        if (chapter == null)
        {
            throw new Exception($"Chapter not found: {chapterId}");
        }

        chapter.IsActive = !chapter.IsActive;
        chapter.UpdatedAt = DateTime.Now;
        // TODO: Get user ID from current user context
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
