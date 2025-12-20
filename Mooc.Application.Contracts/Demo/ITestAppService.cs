using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Application.Contracts.Demo;

public interface ITestAppService : ICrudService<TestOutputDto, TestOutputDto, long, FilterPagedResultRequestDto, CreateTestInputDto, UpdateTestInputDto>
{
}
