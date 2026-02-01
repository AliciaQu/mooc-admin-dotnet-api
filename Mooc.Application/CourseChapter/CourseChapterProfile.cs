using AutoMapper;
using Mooc.Application.Contracts.CourseChapter.Dto;

namespace Mooc.Application.CourseChapter;

/// <summary>
/// AutoMapper configuration for Course Chapter
/// Defines mapping relationships between Entity and DTOs
/// </summary>
public class CourseChapterProfile : Profile
{
    public CourseChapterProfile()
    {
        // Entity -> Output DTO (for query operations)
        CreateMap<Model.Entity.CourseChapter.CourseChapter, CourseChapterOutputDto>();
        
        // Create Input DTO -> Entity (for create operations)
        CreateMap<CreateCourseChapterInputDto, Model.Entity.CourseChapter.CourseChapter>();
        
        // Update Input DTO -> Entity (for update operations)
        CreateMap<UpdateCourseChapterInputDto, Model.Entity.CourseChapter.CourseChapter>();
    }
}