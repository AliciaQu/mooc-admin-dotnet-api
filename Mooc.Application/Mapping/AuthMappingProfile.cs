using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.Application.Mapping
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegistrationDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<User, RegisterOutputDto>();
        }
    }
}
