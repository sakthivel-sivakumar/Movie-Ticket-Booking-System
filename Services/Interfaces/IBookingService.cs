using MovieTicketBooking.DTOs.Bookings;

namespace MovieTicketBooking.Services.Interfaces;

public interface IBookingService
{
    Task<List<BookingDto>> GetAllAsync();

    Task<BookingDto?> GetByIdAsync(int id, int? userId);

    Task<List<BookingDto>> GetByShowAsync(int showId);

    Task<(BookingDto? Booking, string? Error)> CreateAsync(
        CreateBookingDto dto,
        int userId,
        string customerName,
        string customerEmail);

    Task<(bool Success, string? Error)> CancelAsync(
        int id,
        int? userId);
}
