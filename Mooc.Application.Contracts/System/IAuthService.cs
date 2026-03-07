using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Mooc.Application.Contracts.System
{
	internal interface IAuthService 
	{

        Task<RegisterOutputDto> RegisterAsync(RegistrationDto input);
        Task<TokenResponseDto> LoginAsync(LoginDto input);
        Task<TokenResponseDto> RefreshAsync(RefreshTokenRequestDto input);
    }
}
