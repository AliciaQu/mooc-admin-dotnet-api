using Mooc.Application.Contracts;
using Mooc.Core.Attributes;

namespace Mooc.Application.Demo
{

    [DBSeedDataOrder(Order = 1000)]
    public class TestDBSeedDataService : IDBSeedDataService, ITransientDependency
    {

        private readonly MoocDBContext _dbContext;
        public TestDBSeedDataService(MoocDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        private List<Test> tests = new List<Test>()
        {
            new Test() { Id = 1, Title = "test01", Count = 1 },
            new Test() { Id = 2, Title = "test01", Count = 1 },
            new Test() { Id = 3, Title = "test03", Count = 3 },
            new Test() { Id = 4, Title = "test04", Count = 4 },
            new Test() { Id = 5, Title = "test05", Count = 5 },
            new Test() { Id = 6, Title = "test06", Count = 6 },
        };

        public async Task<bool> InitAsync()
        {


            if (!this._dbContext.Tests.Any())
            {
                await this._dbContext.Tests.AddRangeAsync(tests);
                await this._dbContext.SaveChangesAsync();
            }

            return true;
        }
    }
}

