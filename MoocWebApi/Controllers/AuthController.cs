using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mooc.Application.Contracts.Demo;
using Mooc.Application.Demo;
using Mooc.Application.System;
using Mooc.Model.DBContext;
using Mooc.Model.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mooc.Application.Contracts.System;



namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;  

  


  
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("regisetr")]

    public async Task<RegisterOutputDto> Createasys([FromBody] RegistrationDto input)  
    {
        return await _authService.RegisterAsync(input);
    }


    [HttpPost("login")]
    public async Task<TokenResponseDto> Login(LoginDto request)
    {
        return await _authService.LoginAsync(request);
    }

}




