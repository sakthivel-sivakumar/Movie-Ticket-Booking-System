using MovieTicketBooking.DTOs.Movies;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services;

public class MovieService(IMovieRepository repo) : IMovieService
{
    private static MovieDto Map(Movie x)
    {
        return new MovieDto(
            x.MovieId,
            x.Title,
            x.Description,
            x.DurationMinutes,
            x.ReleaseDate,
            x.Language,
            x.Genre
        );
    }

    public async Task<List<MovieDto>> GetAllAsync()
    {
        return [.. (await repo.GetAllAsync()).Select(Map)];
    }

    public async Task<MovieDto?> GetByIdAsync(int id)
    {
        var x = await repo.GetByIdAsync(id);

        return x is null ? null : Map(x);
    }

    public async Task<MovieDto> CreateAsync(CreateMovieDto dto)
    {
        var x = new Movie
        {
            Title = dto.Title!,
            Description = dto.Description!,
            DurationMinutes = dto.DurationMinutes,
            ReleaseDate = dto.ReleaseDate,
            Language = dto.Language!,
            Genre = dto.Genre!
        };

        await repo.AddAsync(x);
        await repo.SaveChangesAsync();

        return Map(x);
    }

    public async Task<MovieDto?> UpdateAsync(int id, UpdateMovieDto dto)
    {
        var x = await repo.GetByIdAsync(id);

        if (x is null)
            return null;

        x.Title = dto.Title!;
        x.Description = dto.Description;
        x.DurationMinutes = dto.DurationMinutes;
        x.ReleaseDate = dto.ReleaseDate;
        x.Language = dto.Language!;
        x.Genre = dto.Genre!;

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
