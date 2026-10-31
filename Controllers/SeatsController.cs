using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Seats;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeatsController(ISeatService service) : ControllerBase
{
    [HttpGet("screen/{screenId:int}")]
    public async Task<IActionResult> ByScreen(int screenId)
    {
        return Ok(await service.GetByScreenAsync(screenId));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var x = await service.GetByIdAsync(id);

        return x is null ? NotFound() : Ok(x);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSeatDto dto)
    {
        var x = await service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                id = x.SeatId
            },
            x
        );
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> Bulk(BulkCreateSeatsDto dto)
    {
        return Ok(await service.CreateBulkAsync(dto));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSeatDto dto)
    {
        var x = await service.UpdateAsync(id, dto);

        return x is null ? NotFound() : Ok(x);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await service.DeleteAsync(id)
            ? NoContent()
            : NotFound();
    }
}
