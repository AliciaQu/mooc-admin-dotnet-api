namespace Mooc.Application.CourseChapter;

/// <summary>
/// 课程章节删除服务实现
/// </summary>
public class CourseChapterDeleteService : DeleteService<Model.Entity.CourseChapter.CourseChapter, long>, ICourseChapterDeleteService, ITransientDependency
{
    public CourseChapterDeleteService()
    {
    }
}
