namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClasAuthController : ControllerBase

{

    private static readonly List<RegistrationDto> users = [
new(
"yes",
"123324@.com",
1234567789,
"male",
12,
"qwertyuiop123!@#"
), new(

"agre",
"54321@.com",
65434321,
"male",
17,
"oiuytrew123!@#"
)
];

  [HttpGet]
    public IActionResult GetAllUsers()
    {
        return Ok(users);
    }

}
