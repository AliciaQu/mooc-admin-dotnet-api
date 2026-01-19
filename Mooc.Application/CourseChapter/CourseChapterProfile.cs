namespace Mooc.Application.CourseChapter;

/// <summary>
/// Course Chapter AutoMapper Configuration
/// </summary>
public class CourseChapterProfile : Profile
{
    public CourseChapterProfile()
    {
        CreateMap<Model.Entity.CourseChapter.CourseChapter, CourseChapterOutputDto>();
        CreateMap<CreateCourseChapterInputDto, Model.Entity.CourseChapter.CourseChapter>();
        CreateMap<UpdateCourseChapterInputDto, Model.Entity.CourseChapter.CourseChapter>();
    }
}
