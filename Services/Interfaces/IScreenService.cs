using MovieTicketBooking.DTOs.Screens;
namespace MovieTicketBooking.Services.Interfaces;

public interface IScreenService
{
    Task<List<ScreenDto>> GetAllAsync();
    Task<ScreenDto?> GetByIdAsync(int id);
    Task<List<ScreenDto>> GetByTheatreAsync(int theatreId);
    Task<ScreenDto> CreateAsync(CreateScreenDto dto);
    Task<ScreenDto?> UpdateAsync(int id, UpdateScreenDto dto);
    Task<bool> DeleteAsync(int id);
}
