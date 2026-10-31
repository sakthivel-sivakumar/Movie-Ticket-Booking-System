using MovieTicketBooking.DTOs.Theatres;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services;

public class TheatreService(ITheatreRepository repo) : ITheatreService
{
    private static TheatreDto Map(Theatre x) =>
        new(x.TheatreId, x.Name, x.Location);

    public async Task<List<TheatreDto>> GetAllAsync()
    {
        return [.. (await repo.GetAllAsync()).Select(Map)];
    }

    public async Task<TheatreDto?> GetByIdAsync(int id)
    {
        var x = await repo.GetByIdAsync(id);

        return x is null ? null : Map(x);
    }

    public async Task<TheatreDto> CreateAsync(CreateTheatreDto dto)
    {
        var x = new Theatre
        {
            Name = dto.Name!,
            Location = dto.Location!
        };

        await repo.AddAsync(x);
        await repo.SaveChangesAsync();

        return Map(x);
    }

    public async Task<TheatreDto?> UpdateAsync(int id, UpdateTheatreDto dto)
    {
        var x = await repo.GetByIdAsync(id);

        if (x is null)
            return null;

        x.Name = dto.Name!;
        x.Location = dto.Location!;

        await repo.SaveChangesAsync();

        return Map(x);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var x = await repo.GetByIdAsync(id);

        if (x is null)
            return false;

        repo.Delete(x);
        await repo.SaveChangesAsync();

        return true;
    }
}
