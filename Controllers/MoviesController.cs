using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.DTOs.Movies;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(IMovieService service) : ControllerBase
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

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string title)
    {
        var all = await service.GetAllAsync();

        var result = all.Where(x =>
            x.Title.Contains(title ?? "", StringComparison.OrdinalIgnoreCase));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMovieDto dto)
    {
        var x = await service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                id = x.MovieId
            },
            x
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateMovieDto dto)
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
