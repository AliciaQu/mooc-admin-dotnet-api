using Microsoft.Extensions.Options;
using Mooc.Application.Contracts.Demo;
using Mooc.Core.Caching;


namespace MoocWebApi.Controllers.Demo
{

    [ApiExplorerSettings(GroupName = nameof(SwaggerGroup.DemoService))]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IMoocCache _moocCache;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UploadFolderOption _uploadFolderOption;
        private readonly ITestReadService _testService;
        private readonly ITestCreateService _testCreateService;
        private readonly ITestUpdateService _testUpdateService;
        private readonly ITestDeleteService _testDeleteService;
      
        public TestController(ILogger<TestController> logger, IMoocCache moocCache,
            IWebHostEnvironment webHostEnvironment,
            IOptions<UploadFolderOption> uploadFolderOption,
            ITestReadService testService,
            ITestCreateService testCreateService,
            ITestUpdateService testUpdateService,
            ITestDeleteService testDeleteService
           )
        {
            _logger = logger;
            _moocCache = moocCache;
            _webHostEnvironment = webHostEnvironment;
            _uploadFolderOption = uploadFolderOption.Value;
            _testService = testService;
            _testCreateService = testCreateService;
            _testUpdateService = testUpdateService;
            _testDeleteService = testDeleteService;
           
        }

        [HttpGet("{id}")]
        public async Task<TestOutputDto> GetAsync(long id)
        {
            return await _testService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<TestOutputDto>> GetPageAsync([FromQuery] FilterPagedResultRequestDto input)
        {
            return await _testService.GetListAsync(input);
        }

        [HttpPost]
        public async Task<TestOutputDto> CreateAsync([FromBody] CreateTestInputDto input)
        {
            return await _testCreateService.CreateAsync(input);

        }

        [HttpPost]
        public async Task<TestOutputDto> UpdateAsync([FromBody] UpdateTestInputDto input)
        {
            return await _testUpdateService.UpdateAsync(input.Id, input);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(long id)
        {
            await _testDeleteService.DeleteAsync(id);
            return true;
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var savePath = Path.Combine(_webHostEnvironment.ContentRootPath, _uploadFolderOption.RootFolder, _uploadFolderOption.AvatarFolder);

            var pathFile = Path.Combine(savePath, file.FileName);

            Directory.CreateDirectory(savePath);

            using (var stream = System.IO.File.Create(pathFile))
            {
                file.CopyTo(stream);
            }

            return Ok(new { file.FileName, file.ContentType, file.Length });
        }
    }
}
