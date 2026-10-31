using MovieTicketBooking.DTOs.Theatres;
namespace MovieTicketBooking.Services.Interfaces;

public interface ITheatreService
{
    Task<List<TheatreDto>> GetAllAsync();
    Task<TheatreDto?> GetByIdAsync(int id);
    Task<TheatreDto> CreateAsync(CreateTheatreDto dto);
    Task<TheatreDto?> UpdateAsync(int id, UpdateTheatreDto dto);
    Task<bool> DeleteAsync(int id);
}
