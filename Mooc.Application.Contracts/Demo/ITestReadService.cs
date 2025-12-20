namespace Mooc.Application.Contracts.Demo;

public interface ITestReadService : IReadOnlyService<TestOutputDto, TestOutputDto, long, FilterPagedResultRequestDto>
{
}
