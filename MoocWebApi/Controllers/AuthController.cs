using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mooc.Application.Contracts.Demo;
using Mooc.Application.Demo;
using Mooc.Model.Entity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class auth (IConfiguration configuration): ControllerBase


{


	private static List<User> _users = new List<User>();




	[HttpPost("registr")]

	public async Task<RegisterOutputDto> Createasys([FromBody]RegistrationDto registrationDto)
   
    {

		var user = new User();

		var Passwordhasher = new PasswordHasher<User>().HashPassword(user, request.Password);
	


		user.UserName = request.UserName;
        user.Password = Passwordhasher;

        user.Phone = request.Phonenumber.ToString();
        user.Email = request.Email;
        if (!string.IsNullOrEmpty(request.Gender)) {
            if (request.Gender.ToLower() == "male") {
                user.Gender = Mooc.Model.Entity.Gender.Male;
            } else if (request.Gender.ToLower() == "female") {
                user.Gender = Mooc.Model.Entity.Gender.Female;
            } else {
                user.Gender = Mooc.Model.Entity.Gender.Other;
            }
        }

		_users.Add(user);
		return Ok(user);
    }


[HttpPost("login")]
    public ActionResult<String> Login(LoginDto Request)
    {
		var user = _users.FirstOrDefault(u => u.UserName == Request.Username);

		var Passwordhasher = new PasswordHasher<User>();
		var result = Passwordhasher.VerifyHashedPassword(user, user.Password, Request.Password);

		if (result == PasswordVerificationResult.Failed)
		{
			return BadRequest("User Name Or Pass word dose not Found.");
		}
		string token = CreatToken(user);


		return Ok(token);
	}

	private string CreatToken(User user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name,user.UserName)

		};
		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(configuration.GetValue<string>("JwtSetting:SecurityKey")!));
		var alg = configuration["JwtSetting:ENAlgorithm"];
		var algorithm = alg == "HS256"
			? SecurityAlgorithms.HmacSha256
			: SecurityAlgorithms.HmacSha256; 

		var creds = new SigningCredentials(key, algorithm);
		var tokenDescriptor = new JwtSecurityToken(
		 issuer: configuration["JwtSetting:Issuer"],
		 audience: configuration["JwtSetting:Audience"],
		 claims: claims,
		 expires: DateTime.UtcNow.AddSeconds(
			 double.Parse(configuration["JwtSetting:ExpireSeconds"]!)),
		 signingCredentials: creds
	 );
		return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);	
	}
    


}




