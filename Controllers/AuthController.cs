using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Auth;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await service.RegisterAsync(dto);

        if (!result.Success)
            return BadRequest(new
            {
                message = result.Message
            });

        return Ok(new
        {
            message = result.Message
        });
    }
}
