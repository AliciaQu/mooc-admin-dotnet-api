namespace Mooc.Application.Demo;

public class TestAppService : CrudService<Test, TestOutputDto, TestOutputDto, long, FilterPagedResultRequestDto, CreateTestInputDto, UpdateTestInputDto>, ITestAppService, ITransientDependency
{
    protected override IQueryable<Test> CreateFilteredQuery(FilterPagedResultRequestDto input)
    {
        if (input != null && !string.IsNullOrWhiteSpace(input.Filter))
        {
            return base.CreateFilteredQuery(input).Where(x => x.Title.Contains(input.Filter));
        }
        return base.CreateFilteredQuery(input);
    }
}
