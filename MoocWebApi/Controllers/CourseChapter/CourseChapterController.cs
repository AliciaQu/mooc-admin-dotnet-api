using Mooc.Application.Contracts.CourseChapter;
using Mooc.Application.Contracts.CourseChapter.Dto;

namespace MoocWebApi.Controllers.CourseChapter;

/// <summary>
/// Course Chapter Management API
/// </summary>
[ApiExplorerSettings(GroupName = nameof(SwaggerGroup.CourseChapterService))]
[Route("api/[controller]/[action]")]
[ApiController]
public class CourseChapterController : ControllerBase
{
    private readonly ILogger<CourseChapterController> _logger;
    private readonly ICourseChapterAppService _courseChapterService;

    public CourseChapterController(
        ILogger<CourseChapterController> logger,
        ICourseChapterAppService courseChapterService)
    {
        _logger = logger;
        _courseChapterService = courseChapterService;
    }

    /// <summary>
    /// Get chapter details
    /// </summary>
    /// <param name="id">Chapter ID</param>
    [HttpGet("{id}")]
    public async Task<CourseChapterOutputDto> GetAsync(long id)
    {
        return await _courseChapterService.GetAsync(id);
    }

    /// <summary>
    /// Get chapter list (paginated)
    /// </summary>
    [HttpGet]
    public async Task<PagedResultDto<CourseChapterOutputDto>> GetPageAsync([FromQuery] FilterPagedResultRequestDto input)
    {
        return await _courseChapterService.GetListAsync(input);
    }

    /// <summary>
    /// Get all chapters by course ID
    /// </summary>
    /// <param name="courseId">Course ID</param>
    [HttpGet("{courseId}")]
    public async Task<List<CourseChapterOutputDto>> GetByCourseIdAsync(long courseId)
    {
        return await _courseChapterService.GetChaptersByCourseIdAsync(courseId);
    }

    /// <summary>
    /// Create chapter
    /// </summary>
    [HttpPost]
    public async Task<CourseChapterOutputDto> CreateAsync([FromBody] CreateCourseChapterInputDto input)
    {
        return await _courseChapterService.CreateAsync(input);
    }

    /// <summary>
    /// Update chapter
    /// </summary>
    [HttpPost]
    public async Task<CourseChapterOutputDto> UpdateAsync([FromBody] UpdateCourseChapterInputDto input)
    {
        return await _courseChapterService.UpdateAsync(input.Id, input);
    }

    /// <summary>
    /// Delete chapter
    /// </summary>
    /// <param name="id">Chapter ID</param>
    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(long id)
    {
        await _courseChapterService.DeleteAsync(id);
        return true;
    }

    /// <summary>
    /// Update chapter order
    /// </summary>
    /// <param name="id">Chapter ID</param>
    /// <param name="newOrder">New order index</param>
    [HttpPost]
    public async Task<bool> UpdateOrderAsync(long id, [FromBody] int newOrder)
    {
        await _courseChapterService.UpdateChapterOrderAsync(id, newOrder);
        return true;
    }

    /// <summary>
    /// Toggle chapter status (active/inactive)
    /// </summary>
    /// <param name="id">Chapter ID</param>
    [HttpPost]
    public async Task<bool> ToggleStatusAsync(long id)
    {
        await _courseChapterService.ToggleChapterStatusAsync(id);
        return true;
    }
}
