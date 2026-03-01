using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mooc.Application.Contracts.Demo;
using Mooc.Application.Demo;
using Mooc.Model.Entity;



namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{

	private static readonly List<User> _users = new();
	private readonly IOptions<JwtSettings> _jwtSettings;

	public AuthController(IOptions<JwtSettings> jwtSettings)
	{
		_jwtSettings = jwtSettings;
	}


	[HttpPost("regisetr")]

	public async Task<RegisterOutputDto> Createasys([FromBody]RegistrationDto input)
   
    {

		var user = new User();


		var Passwordhasher = new PasswordHasher<User>().HashPassword(user, input.Password);

	

		user.UserName = input.UserName;
        user.Password = Passwordhasher;

        user.Phone = input.Phone.ToString();
        user.Email = input.Email;
        if (!string.IsNullOrEmpty(input.Gender)) {
            if (input.Gender.ToLower() == "male") {
                user.Gender = Mooc.Model.Entity.Gender.Male;
            } else if (input.Gender.ToLower() == "female") {
                user.Gender = Mooc.Model.Entity.Gender.Female;
            } else {
                user.Gender = Mooc.Model.Entity.Gender.Other;
            }
        }

		_users.Add(user);



		var output = new RegisterOutputDto
		{
			UserName = user.UserName,
			Email = user.Email,
			Phone = user.Phone,
			Gender = input.Gender,
			Dob = input.Dob
		};

		return output;
	}


	[HttpPost("login")]
	public async Task<string> Login(LoginDto Request)



    {
		var user = _users.FirstOrDefault(u => u.UserName == Request.Username);

		var Passwordhasher = new PasswordHasher<User>();
		var result = Passwordhasher.VerifyHashedPassword(user, user.Password, Request.Password);

		if (result == PasswordVerificationResult.Failed)
		{
			return ("User Name Or Pass word dose not Found.");
		}
		string token = CreatToken(user);


		return(token);
	}

	private string CreatToken(User user)
	{
		var settings = _jwtSettings.Value;
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name,user.UserName)

		};
		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(settings.SecurityKey));
		var alg = settings.ENAlgorithm;
		var algorithm = alg == "HS256"
			? SecurityAlgorithms.HmacSha256
			: SecurityAlgorithms.HmacSha256; 

		var creds = new SigningCredentials(key, algorithm);
		var tokenDescriptor = new JwtSecurityToken(
		issuer: settings.Issuer,
		audience: settings.Audience,
		 claims: claims,

		  expires: DateTime.UtcNow.AddSeconds(settings.ExpireSeconds), 
	signingCredentials: creds
	 );
		return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);	
	}
    


}




