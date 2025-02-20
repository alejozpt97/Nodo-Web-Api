using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Models;
using WebApi.Data;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController(ApplicationDbContext context) : ControllerBase
{
   private readonly UserService userService = new(context);

   [HttpGet("")]
   public async Task<IActionResult> GetAllUsers()
   {
        var users = await userService.GetAllUsers();

         return Ok(users);
   }

    [HttpGet("{id}")]
   public async Task<IActionResult> GetUserById(int id)
   {
      var User = await userService.GetUserById(id);

       if (User == null)
      {
          return NotFound(new ErrorResponse {Message = "User not found", StatusCode = 404});
      }

      return Ok(User);
   }

   [HttpPost]
   public async Task<IActionResult> CreateUser([FromBody] User user)   
    {
    try
        {
        var createdUser = await userService.CreateUser(user);
        return Created("/", createdUser);
        }
    catch (Exception ex)
        {
        return BadRequest(new { Error = ex.Message });
        }
    }

      [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserById(User user, int id)
    {  
        try {

        await userService.UpdateUserById(id, user);

        return Ok(new { Message = "User updated successfully", StatusCode = 200 });

        }
        catch (Exception ex)
        {
        return BadRequest(new { Error = ex.Message });
        }

    }

      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteUserById(int id)
   {
     var userRemoved = await userService.DeleteUserById(id);
     
       if (userRemoved == null)
      {
          return NotFound(new ErrorResponse {Message = "User not found", StatusCode = 404});
      }

      return Ok(new { Message = "User removed successfully", StatusCode = 200 });

      }
}
