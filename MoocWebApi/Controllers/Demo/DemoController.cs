using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mooc.Application.Contracts.Demo;
using Mooc.Application.Demo;

namespace MoocWebApi.Controllers.Demo
{
    [ApiExplorerSettings(GroupName = nameof(SwaggerGroup.DemoService))]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ITestAppService _testAppService;
        public DemoController(ITestAppService testAppService)
        {
            this._testAppService = testAppService;
        }

        [HttpGet("{id}")]
        public async Task<TestOutputDto> GetAsync(long id)
        {
            return await _testAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<TestOutputDto>> GetPageAsync([FromQuery] FilterPagedResultRequestDto input)
        {
            return await _testAppService.GetListAsync(input);
        }

        [HttpPost]
        public async Task<TestOutputDto> CreateAsync([FromBody] CreateTestInputDto input)
        {
            return await _testAppService.CreateAsync(input);

        }

        [HttpPost]
        public async Task<TestOutputDto> UpdateAsync([FromBody] UpdateTestInputDto input)
        {
            return await _testAppService.UpdateAsync(input.Id, input);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(long id)
        {
            await _testAppService.DeleteAsync(id);
            return true;
        }
    }
}
