
namespace Mooc.Application.Demo
{
    public class TestReadService : ReadOnlyService<Test, TestOutputDto, long, FilterPagedResultRequestDto>, ITestReadService, ITransientDependency
    {
        public TestReadService()
        {
        }

        protected override IQueryable<Test> CreateFilteredQuery(FilterPagedResultRequestDto input)
        {
            if (input != null && !string.IsNullOrWhiteSpace(input.Filter))
            {
                return base.CreateFilteredQuery(input).Where(x => x.Title.Contains(input.Filter));
            }
            return base.CreateFilteredQuery(input);
        }
    }


}
