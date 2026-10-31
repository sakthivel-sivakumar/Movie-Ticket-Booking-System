using MovieTicketBooking.DTOs.Shows;
namespace MovieTicketBooking.Services.Interfaces;

public interface IShowService
{
    Task<List<ShowDto>> GetAllAsync(DateTime? date, int? movieId, int? theatreId);
    Task<ShowDto?> GetByIdAsync(int id);
    Task<ShowDto?> CreateAsync(CreateShowDto dto);
    Task<ShowDto?> UpdateAsync(int id, UpdateShowDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<ShowSeatDto>?> GetSeatsAsync(int showId, string? status);
}
