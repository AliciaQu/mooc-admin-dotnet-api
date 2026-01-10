namespace Mooc.Application.CourseChapter;

/// <summary>
/// 课程章节创建服务实现
/// </summary>
public class CourseChapterCreateService : CreateService<Model.Entity.CourseChapter.CourseChapter, CreateCourseChapterInputDto, CourseChapterOutputDto>, ICourseChapterCreateService, ITransientDependency
{
    public CourseChapterCreateService()
    {
    }

    protected override async Task<Model.Entity.CourseChapter.CourseChapter> MapToEntityAsync(CreateCourseChapterInputDto createInput)
    {
        var entity = await base.MapToEntityAsync(createInput);
        
        // 设置创建时间和创建人
        entity.CreatedAt = DateTime.Now;
        // TODO: 从当前用户上下文获取用户ID
        entity.CreatedBy = 1; 
        
        return entity;
    }
}
