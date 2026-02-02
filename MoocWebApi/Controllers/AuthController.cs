using Microsoft.AspNetCore.Mvc;
using Mooc.Application.Contracts.Demo;
using Mooc.Application.Demo;

namespace MoocWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class auth : ControllerBase


{

    private static readonly List<RegistrationDto> users = [

 new  RegistrationDto(
        Id :  1,
        UserNmae: "yes",
        Email: "123324@.com",
        PhoneNumber: 1234567789,
        Gender: "male",
        Age: 12,
        PassWord: "qwertyuiop123!@#"
    ),
    new  RegistrationDto(
        Id : 2,
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

    [HttpPost]
    public IActionResult CreateAsync([FromBody] RegistrationDto input)
    {

        var newId = users.Count != 0 ? users.Max(u => u.Id) + 1 : 1;
        var newUser = new RegistrationDto(
            Id: newId,
             input.UserName,
             input.Email,
             input.PhoneNumber,
             input.Gender,
             input.Age,
             input.PassWord
        );

        users.Add(newUser);

        return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAsync(int id, [FromBody] RegistrationDto input)
    {
        var index = users.FindIndex(u => u.Id == id);



        var updatedUser = users[index] with
        {
			UserName = input.UserName,
            Email = input.Email,
            PhoneNumber = input.PhoneNumber,
            Gender = input.Gender,
            Age = input.Age,
            PassWord = input.PassWord
        };

        users[index] = updatedUser;

        return Ok(updatedUser);



    }
    [HttpDelete("{id}")]
  
    public IActionResult Delete(int id)
    {

        var DeleteItem = users.FindIndex(u => u.Id == id);
        if (DeleteItem == -1)


            return NotFound("Sorry, We dont have this User");

        users.RemoveAt(DeleteItem);
        return NoContent();

    }

}
