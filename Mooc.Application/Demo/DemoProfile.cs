namespace Mooc.Application.Demo
{
    /// <summary>
    /// automapper configs
    /// </summary>
    public class DemoProfile : Profile
    {
        public DemoProfile()
        {
            CreateMap<Test, TestOutputDto>();
            CreateMap<CreateTestInputDto, Test>();
            CreateMap<UpdateTestInputDto, Test>();
        }
       
    }
}
