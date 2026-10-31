using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Screens;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScreensController(IScreenService service) : ControllerBase
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

    [HttpGet("theatre/{theatreId:int}")]
    public async Task<IActionResult> ByTheatre(int theatreId)
    {
        return Ok(await service.GetByTheatreAsync(theatreId));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateScreenDto dto)
    {
        var x = await service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                id = x.ScreenId
            },
            x
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateScreenDto dto)
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
