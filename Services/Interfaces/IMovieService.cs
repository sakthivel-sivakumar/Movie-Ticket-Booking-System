using MovieTicketBooking.DTOs.Movies;
namespace MovieTicketBooking.Services.Interfaces;

public interface IMovieService
{
    Task<List<MovieDto>> GetAllAsync();
    Task<MovieDto?> GetByIdAsync(int id);
    Task<MovieDto> CreateAsync(CreateMovieDto dto);
    Task<MovieDto?> UpdateAsync(int id, UpdateMovieDto dto);
    Task<bool> DeleteAsync(int id);
}
