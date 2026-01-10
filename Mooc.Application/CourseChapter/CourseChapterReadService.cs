namespace Mooc.Application.CourseChapter;

/// <summary>
/// 课程章节读取服务实现
/// </summary>
public class CourseChapterReadService : ReadOnlyService<Model.Entity.CourseChapter.CourseChapter, CourseChapterOutputDto, long, FilterPagedResultRequestDto>, ICourseChapterReadService, ITransientDependency
{
    public CourseChapterReadService()
    {
    }

    protected override IQueryable<Model.Entity.CourseChapter.CourseChapter> CreateFilteredQuery(FilterPagedResultRequestDto input)
    {
        var query = base.CreateFilteredQuery(input);

        if (input != null && !string.IsNullOrWhiteSpace(input.Filter))
        {
            query = query.Where(x => x.ChapterName.Contains(input.Filter) || x.Description.Contains(input.Filter));
        }

        return query.OrderBy(x => x.CourseId).ThenBy(x => x.OrderIndex);
    }

    /// <summary>
    /// 根据课程ID获取所有章节
    /// </summary>
    public async Task<List<CourseChapterOutputDto>> GetChaptersByCourseIdAsync(long courseId)
    {
        var chapters = await Repository.GetQueryableAsync();
        var filteredChapters = chapters.Where(x => x.CourseId == courseId)
                                      .OrderBy(x => x.OrderIndex)
                                      .ToList();
        
        return ObjectMapper.Map<List<Model.Entity.CourseChapter.CourseChapter>, List<CourseChapterOutputDto>>(filteredChapters);
    }
}
