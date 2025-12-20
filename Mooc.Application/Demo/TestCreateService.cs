namespace Mooc.Application.Demo;

public class TestCreateService : CreateService<Test, TestOutputDto, CreateTestInputDto, long>, ITestCreateService, ITransientDependency
{
    public TestCreateService()
    {
    }
}
