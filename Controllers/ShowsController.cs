using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Constants;
using MovieTicketBooking.DTOs.Shows;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowsController(IShowService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] DateTime? date,
        [FromQuery] int? movieId,
        [FromQuery] int? theatreId)
    {
        return Ok(await service.GetAllAsync(date, movieId, theatreId));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var x = await service.GetByIdAsync(id);

        return x is null ? NotFound() : Ok(x);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateShowDto dto)
    {
        var x = await service.CreateAsync(dto);

        return x is null
            ? BadRequest("Invalid time range or ticket price.")
            : CreatedAtAction(
                nameof(GetById),
                new
                {
                    id = x.ShowId
                },
                x
            );
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateShowDto dto)
    {
        var x = await service.UpdateAsync(id, dto);

        return x is null
            ? BadRequest("Show not found or invalid time range.")
            : Ok(x);
    }

    [Authorize(Roles = UserRoles.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await service.DeleteAsync(id)
            ? NoContent()
            : NotFound();
    }

    [HttpGet("{showId:int}/seats")]
    public async Task<IActionResult> Seats(int showId)
    {
        return Ok(await service.GetSeatsAsync(showId, null));
    }

    [HttpGet("{showId:int}/seats/available")]
    public async Task<IActionResult> Available(int showId)
    {
        return Ok(await service.GetSeatsAsync(showId, "Available"));
    }

    [HttpGet("{showId:int}/seats/booked")]
    public async Task<IActionResult> Booked(int showId)
    {
        return Ok(await service.GetSeatsAsync(showId, "Booked"));
    }
}
