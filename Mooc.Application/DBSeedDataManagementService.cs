using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mooc.Application.Contracts;
using Mooc.Core.Attributes;
using Mooc.Model.Option;
using System.Reflection;

namespace Mooc.Application
{
    public class DBSeedDataManagementService : IDBSeedDataManagementService,ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DataBaseOption _dataBaseOption;
        public DBSeedDataManagementService(IServiceProvider serviceProvider, IOptions< DataBaseOption> dataBaseOption) 
        { 
            _serviceProvider = serviceProvider;
            _dataBaseOption = dataBaseOption.Value;
        }    
        public async Task IntiAsync()
        {

            if (_dataBaseOption?.DataSeed == true)
            {
             
                using (var socpe = _serviceProvider.CreateScope())
                {
                    var dbSeedDataSevices = socpe.ServiceProvider.GetRequiredService<IEnumerable<IDBSeedDataService>>();
                    var orderList = dbSeedDataSevices.OrderBy(x =>
                    {
                        var attr = x.GetType().GetCustomAttribute<DBSeedDataOrderAttribute>();
                        return attr?.Order ?? 0;
                    }).ToList();

                    foreach (var dbSeedDataSevice in orderList)
                    {
                        await dbSeedDataSevice.InitAsync();
                    }
                }
            }
        }
    }
}
