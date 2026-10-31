using MovieTicketBooking.DTOs.Bookings;
namespace MovieTicketBooking.Services.Interfaces;

public interface IBookingService
{
    Task<List<BookingDto>> GetAllAsync();
    Task<BookingDto?> GetByIdAsync(int id);
    Task<List<BookingDto>> GetByShowAsync(int showId);
    Task<(BookingDto? Booking, string? Error)> CreateAsync(CreateBookingDto dto);
    Task<(bool Success, string? Error)> CancelAsync(int id);
}
