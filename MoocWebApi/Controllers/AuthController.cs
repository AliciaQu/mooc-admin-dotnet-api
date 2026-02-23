using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mooc.Application.Contracts.Demo;
using Mooc.Model.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EntityGender = Mooc.Model.Entity.Gender;

namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    private static readonly List<User> _users = new();

    [HttpPost("register")]
    public ActionResult<RegisterOutputDto> Register([FromBody] RegistrationDto registrationDto)
    {
        if (_users.Any(u => u.UserName == registrationDto.UserName))
            return BadRequest("Username already exists.");

        var user = new User();
        var passwordHasher = new PasswordHasher<User>();

        user.UserName = registrationDto.UserName;
        user.Password = passwordHasher.HashPassword(user, registrationDto.Password);
        user.Phone = registrationDto.Phonenumber.ToString();
        user.Email = registrationDto.Email;

        if (!string.IsNullOrEmpty(registrationDto.Gender))
        {
            user.Gender = registrationDto.Gender.ToLower() switch
            {
                "male" => EntityGender.Male,
                "female" => EntityGender.Female,
                _ => EntityGender.Other
            };
        }

        _users.Add(user);

        var output = new RegisterOutputDto
        {
            UserName = user.UserName,
            Email = user.Email
        };

        return Ok(output);
    }

    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] LoginDto request)
    {
        var user = _users.FirstOrDefault(u => u.UserName == request.Username);
        if (user == null)
            return BadRequest("Username or password is incorrect.");

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return BadRequest("Username or password is incorrect.");

        string token = CreateToken(user);
        return Ok(token);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSetting:SecurityKey")!));

        var alg = configuration["JwtSetting:ENAlgorithm"];
        var algorithm = alg == "HS512"
            ? SecurityAlgorithms.HmacSha512
            : SecurityAlgorithms.HmacSha256;

        var creds = new SigningCredentials(key, algorithm);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSetting:Issuer"],
            audience: configuration["JwtSetting:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(
                                    double.Parse(configuration["JwtSetting:ExpireSeconds"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

