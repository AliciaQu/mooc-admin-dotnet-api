using Mooc.Application.Contracts.CourseChapter;

namespace MoocWebApi.Controllers.CourseChapter;

/// <summary>
/// 课程章节管理API
/// </summary>
[ApiExplorerSettings(GroupName = nameof(SwaggerGroup.CourseChapterService))]
[Route("api/[controller]/[action]")]
[ApiController]
public class CourseChapterController : ControllerBase
{
    private readonly ILogger<CourseChapterController> _logger;
    private readonly ICourseChapterReadService _readService;
    private readonly ICourseChapterCreateService _createService;
    private readonly ICourseChapterUpdateService _updateService;
    private readonly ICourseChapterDeleteService _deleteService;

    public CourseChapterController(
        ILogger<CourseChapterController> logger,
        ICourseChapterReadService readService,
        ICourseChapterCreateService createService,
        ICourseChapterUpdateService updateService,
        ICourseChapterDeleteService deleteService)
    {
        _logger = logger;
        _readService = readService;
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    /// <summary>
    /// 获取章节详情
    /// </summary>
    /// <param name="id">章节ID</param>
    [HttpGet("{id}")]
    public async Task<CourseChapterOutputDto> GetAsync(long id)
    {
        return await _readService.GetAsync(id);
    }

    /// <summary>
    /// 获取章节列表（分页）
    /// </summary>
    [HttpGet]
    public async Task<PagedResultDto<CourseChapterOutputDto>> GetPageAsync([FromQuery] FilterPagedResultRequestDto input)
    {
        return await _readService.GetListAsync(input);
    }

    /// <summary>
    /// 根据课程ID获取所有章节
    /// </summary>
    /// <param name="courseId">课程ID</param>
    [HttpGet("{courseId}")]
    public async Task<List<CourseChapterOutputDto>> GetByCourseIdAsync(long courseId)
    {
        return await _readService.GetChaptersByCourseIdAsync(courseId);
    }

    /// <summary>
    /// 创建章节
    /// </summary>
    [HttpPost]
    public async Task<CourseChapterOutputDto> CreateAsync([FromBody] CreateCourseChapterInputDto input)
    {
        return await _createService.CreateAsync(input);
    }

    /// <summary>
    /// 更新章节
    /// </summary>
    [HttpPost]
    public async Task<CourseChapterOutputDto> UpdateAsync([FromBody] UpdateCourseChapterInputDto input)
    {
        return await _updateService.UpdateAsync(input.Id, input);
    }

    /// <summary>
    /// 删除章节
    /// </summary>
    /// <param name="id">章节ID</param>
    [HttpDelete("{id}")]
    public async Task<bool> DeleteAsync(long id)
    {
        await _deleteService.DeleteAsync(id);
        return true;
    }

    /// <summary>
    /// 更新章节顺序
    /// </summary>
    /// <param name="id">章节ID</param>
    /// <param name="newOrder">新的顺序号</param>
    [HttpPost]
    public async Task<bool> UpdateOrderAsync(long id, [FromBody] int newOrder)
    {
        await _updateService.UpdateChapterOrderAsync(id, newOrder);
        return true;
    }

    /// <summary>
    /// 切换章节状态（启用/禁用）
    /// </summary>
    /// <param name="id">章节ID</param>
    [HttpPost]
    public async Task<bool> ToggleStatusAsync(long id)
    {
        await _updateService.ToggleChapterStatusAsync(id);
        return true;
    }
}
