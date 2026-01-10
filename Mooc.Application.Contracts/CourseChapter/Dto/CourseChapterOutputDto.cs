namespace Mooc.Application.Contracts.CourseChapter.Dto;

/// <summary>
/// 课程章节输出DTO
/// </summary>
public class CourseChapterOutputDto : BaseEntityDto
{
    public long CourseId { get; set; }
    
    public string ChapterName { get; set; }
    
    public string Description { get; set; }
    
    public int OrderIndex { get; set; }
    
    public int Duration { get; set; }
    
    public bool IsActive { get; set; }
    
    public bool IsFree { get; set; }
    
    public string VideoUrl { get; set; }
    
    public string MaterialUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}
