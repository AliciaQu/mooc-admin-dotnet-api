

namespace Mooc.Application.Demo;

public class TestDeleteService : DeleteService<Test, long>, ITestDeleteService, ITransientDependency
{
    public TestDeleteService(MoocDBContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
