using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mooc.Application.Contracts.Dto;
using Mooc.Application.Contracts.System;
using Mooc.Model.DBContext;
using Mooc.Model.Entity;

namespace Mooc.Application.System  
{
    public class AuthService : IAuthService  
    {
        private readonly MoocDBContext _context;

        public AuthService(MoocDBContext context)
        {
            _context = context;
        }

        public async Task<RegisterOutputDto> RegisterAsync(RegistrationDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenResponseDto> RefreshAsync(RefreshTokenRequestDto input)
        {
            throw new NotImplementedException();
        }
    }
}
