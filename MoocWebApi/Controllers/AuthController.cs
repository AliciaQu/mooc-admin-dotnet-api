using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mooc.Application.Contracts.Demo;
using Mooc.Application.Demo;
using Mooc.Model.Entity;
using Microsoft.IdentityModel.Tokens;

namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class auth : ControllerBase


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

		// If user not found
		if (user == null)
		{
			return BadRequest("User Name Or Pass word dose not Found.");
		}

		var Passwordhasher = new PasswordHasher<User>();
		var result = Passwordhasher.VerifyHashedPassword(user, user.PassWord, Request.Password);

		if (result == PasswordVerificationResult.Failed)
		{
			return BadRequest("User Name Or Pass word dose not Found.");
		}
		string token = "This is working good";


		return Ok(token);
	}
 

    


}




