using MovieTicketBooking.DTOs.Seats;
namespace MovieTicketBooking.Services.Interfaces;

public interface ISeatService
{
    Task<List<SeatDto>> GetByScreenAsync(int screenId);
    Task<SeatDto?> GetByIdAsync(int id);
    Task<SeatDto> CreateAsync(CreateSeatDto dto);
    Task<List<SeatDto>> CreateBulkAsync(BulkCreateSeatsDto dto);
    Task<SeatDto?> UpdateAsync(int id, UpdateSeatDto dto);
    Task<bool> DeleteAsync(int id);
}
