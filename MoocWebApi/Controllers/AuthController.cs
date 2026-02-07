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
    public ActionResult<User> Register(RegistrationDto request)
    {

		var user = new User();

		var Passwordhasher = new PasswordHasher<User>().HashPassword(user, request.Password);
	


		user.UserName = request.UserName;
        user.PassWord = Passwordhasher;

        user.Age = request.Age;

        user.PhoneNumber = request.Phonenumber;
        user.Email = request.Email;
        user.Gender = request.Gender;

		_users.Add(user);
		return Ok(user);
    }


[HttpPost("login")]
    public ActionResult<String> Login(LoginDto Request)
    {
		var user = _users.FirstOrDefault(u => u.UserName == Request.Username);

		var Passwordhasher = new PasswordHasher<User>();
		var result = Passwordhasher.VerifyHashedPassword(user, user.PassWord, Request.Password);

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
			Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSetting:Token")!));

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
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




