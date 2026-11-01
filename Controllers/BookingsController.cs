using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Constants;
using MovieTicketBooking.DTOs.Bookings;
using MovieTicketBooking.Services.Interfaces;
using System.Security.Claims;

namespace MovieTicketBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(IBookingService service) : ControllerBase
{
    private int? GetCurrentUserId()
    {
        var value = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        return int.TryParse(value, out var userId)
            ? userId
            : null;
    }

    // Admin can view all bookings
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await service.GetAllAsync());
    }

    // Customer can view own booking.
    // Admin can view any booking.
    [Authorize(Roles = UserRoles.Customer + "," + UserRoles.Admin)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        int? userId = User.IsInRole(UserRoles.Admin)
            ? null
            : GetCurrentUserId();

        if (userId is null && !User.IsInRole(UserRoles.Admin))
            return Unauthorized();

        var booking = await service.GetByIdAsync(id, userId);

        return booking is null
            ? NotFound()
            : Ok(booking);
    }

    // Admin can view bookings for a particular show
    [Authorize(Roles = UserRoles.Admin)]
    [HttpGet("show/{showId:int}")]
    public async Task<IActionResult> ByShow(int showId)
    {
        return Ok(await service.GetByShowAsync(showId));
    }

    // Customer/Admin can create bookings
    [Authorize(Roles = UserRoles.Customer + "," + UserRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingDto dto)
    {
        var userId = GetCurrentUserId();

        if (userId is null)
            return Unauthorized();

        var name = User.FindFirstValue(ClaimTypes.Name);
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (name is null || email is null)
            return Unauthorized();

        var result = await service.CreateAsync(
            dto,
            userId.Value,
            name,
            email);

        if (result.Error is not null)
        {
            return BadRequest(new
            {
                message = result.Error
            });
        }

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                id = result.Booking!.BookingId
            },
            result.Booking);
    }

    // Customer can cancel own booking.
    // Admin can cancel any booking.
    [Authorize(Roles = UserRoles.Customer + "," + UserRoles.Admin)]
    [HttpPut("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        int? userId = User.IsInRole(UserRoles.Admin)
            ? null
            : GetCurrentUserId();

        if (userId is null && !User.IsInRole(UserRoles.Admin))
            return Unauthorized();

        var result = await service.CancelAsync(id, userId);

        return result.Success
            ? NoContent()
            : BadRequest(new
            {
                message = result.Error
            });
    }
}
