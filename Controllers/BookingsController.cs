using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Bookings;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingsController(IBookingService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var x = await service.GetByIdAsync(id);

        return x is null ? NotFound() : Ok(x);
    }

    [HttpGet("show/{showId:int}")]
    public async Task<IActionResult> ByShow(int showId)
    {
        return Ok(await service.GetByShowAsync(showId));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingDto dto)
    {
        var r = await service.CreateAsync(dto);

        if (r.Error is not null)
            return BadRequest(new
            {
                message = r.Error
            });

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                id = r.Booking!.BookingId
            },
            r.Booking
        );
    }

    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var r = await service.CancelAsync(id);

        return r.Success
            ? NoContent()
            : BadRequest(new
            {
                message = r.Error
            });
    }
}
