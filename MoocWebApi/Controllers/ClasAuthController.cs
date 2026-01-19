using Mooc.Application.Contracts.Demo;
using Mooc.Application.Demo;

namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClasAuthController : ControllerBase

{

    private static readonly List<RegistrationDto> users = [

 new RegistrationDto(
        Id: 1,
        UserNmae: "yes",
        Email: "123324@.com",
        PhoneNumber: 1234567789,
        Gender: "male",
        Age: 12,
        PassWord: "qwertyuiop123!@#"
    ),
    new RegistrationDto(
        Id: 2,
        UserNmae: "agre",
        Email: "54321@.com",
        PhoneNumber: 65434321,
        Gender: "male",
        Age: 17,
        PassWord: "oiuytrew123!@#"
)
];

   
    [HttpGet("{id:int}")]
     public IActionResult GetById(int id) 
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return NotFound("User not found");

        return Ok(user);
    }
    

}
