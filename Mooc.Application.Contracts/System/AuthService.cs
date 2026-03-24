using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mooc.Application.Contracts.Dto;
using Mooc.Application.Contracts.System;
using Mooc.Core.ExceptionHandling;
using Mooc.Model.DBContext;
using Mooc.Model.Entity;
using Mooc.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Mooc.Application.System  
{
    public class AuthService : IAuthService
    {


        private readonly MoocDBContext _context;
        private readonly IOptions<JwtSettings> _jwtSettings;

        public AuthService(MoocDBContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        public async Task<RegisterOutputDto> RegisterAsync(RegistrationDto input)
        {

            var existingUser = await _context.Users
        .FirstOrDefaultAsync(u => u.UserName == input.UserName);
            if (existingUser != null)
                throw new EntityAlreadyExistsException("Username already taken.");

          
            var existingEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == input.Email);
            if (existingEmail != null)
                throw new EntityAlreadyExistsException("Email already taken.");
            var user = new User();
            user.Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            user.UserName = input.UserName;
            user.Phone = input.Phone;
            user.Email = input.Email;
            user.FirstName = input.UserName;
            user.LastName = input.UserName;
            user.CreatedAt = DateTime.UtcNow;
            user.Address = "";
            user.Avatar = "";
            user.Bio = "";
            user.Password = new PasswordHasher<User>().HashPassword(user, input.Password);

            _context.Add(user);
            await _context.SaveChangesAsync();

            return new RegisterOutputDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = input.Gender,
                Dob = input.Dob
            };
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto input)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserName == input.Username);
            if (user == null)
                throw new EntityNotFoundException("User not found.");

            var result = new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.Password, input.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new UserFriendlyException("Username or password not found.");

            string token = CreateToken(user);
            var refreshToken = await CreateRefreshToken(user.Id);

            return new TokenResponseDto
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponseDto> RefreshAsync(RefreshTokenRequestDto input)
        {
            var token = await _context.RefreshTokens
                .Include(rt => rt.User)
                    .ThenInclude(u => u.Roles)
                .FirstOrDefaultAsync(rt => rt.Token == input.RefreshToken);

            if (token == null || token.IsUsed || token.ExpiryDate < DateTime.UtcNow)
                throw new Exception("Invalid or expired refresh token.");

            token.IsUsed = true;

            var newAccessToken = CreateToken(token.User);
            var newRefreshToken = await CreateRefreshToken(token.UserId);

            await _context.SaveChangesAsync();

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        private string CreateToken(User user)
        {
            var settings = _jwtSettings.Value;
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(settings.ExpireSeconds),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<string> CreateRefreshToken(long userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsUsed = false,
                UserId = userId
            };
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken.Token;
        }
        

    }

}
