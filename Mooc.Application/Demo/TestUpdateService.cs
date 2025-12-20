namespace Mooc.Application.Demo;

public class TestUpdateService : UpdateService<Test, TestOutputDto, long, UpdateTestInputDto>, ITestUpdateService, ITransientDependency
{
    public TestUpdateService()
    {
    }
}

