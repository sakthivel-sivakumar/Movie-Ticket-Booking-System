using MovieTicketBooking.DTOs.Bookings;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services;

public class BookingService(IBookingRepository repo) : IBookingService
{
    private static BookingDto Map(Booking x)
    {
        return new BookingDto(
            x.BookingId,
            x.BookingReference,
            x.ShowId,
            x.CustomerName,
            x.CustomerEmail,
            x.CustomerPhone,
            x.BookingDate,
            x.TotalAmount,
            x.Status,
            [
                .. x.BookingSeats.Select(s => new BookingSeatDto(
                    s.SeatId,
                    s.Seat?.RowName ?? "",
                    s.Seat?.SeatNumber ?? 0,
                    s.Price
                ))
            ]
        );
    }

    public async Task<List<BookingDto>> GetAllAsync()
    {
        return [.. (await repo.GetAllAsync()).Select(Map)];
    }

    public async Task<BookingDto?> GetByIdAsync(int id)
    {
        var x = await repo.GetByIdAsync(id);

        return x is null ? null : Map(x);
    }

    public async Task<List<BookingDto>> GetByShowAsync(int id)
    {
        return [.. (await repo.GetByShowAsync(id)).Select(Map)];
    }

    public async Task<(BookingDto? Booking, string? Error)> CreateAsync(
        CreateBookingDto dto)
    {
        if (dto.SeatIds is null || dto.SeatIds.Count == 0)
            return (null, "Choose at least one seat.");

        if (dto.SeatIds.Distinct().Count() != dto.SeatIds.Count)
            return (null, "Duplicate seat IDs are not allowed.");

        var show = await repo.GetShowAsync(dto.ShowId);

        if (show is null)
            return (null, "Show not found.");

        var rows = await repo.GetShowSeatsAsync(dto.ShowId, dto.SeatIds);

        if (rows.Count != dto.SeatIds.Count)
            return (null, "A selected seat does not belong to this show.");

        if (rows.Any(x => x.Status != "Available"))
            return (null, "One or more selected seats are already booked.");

        var booking = new Booking
        {
            BookingReference = "MTB-" +
                Guid.NewGuid().ToString("N")[..12].ToUpperInvariant(),

            ShowId = dto.ShowId,
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            CustomerPhone = dto.CustomerPhone,
            BookingDate = DateTime.UtcNow,
            TotalAmount = show.TicketPrice * rows.Count,
            Status = "Confirmed",

            BookingSeats =
            [
                .. rows.Select(x => new BookingSeat
                {
                    SeatId = x.SeatId,
                    Price = show.TicketPrice
                })
            ]
        };

        await repo.SaveBookingAsync(booking, rows);

        return (await GetByIdAsync(booking.BookingId), null);
    }

    public async Task<(bool Success, string? Error)> CancelAsync(int id)
    {
        var booking = await repo.GetByIdAsync(id);

        if (booking is null)
            return (false, "Booking not found.");

        if (booking.Status == "Cancelled")
            return (false, "Booking is already cancelled.");

        var rows = await repo.GetShowSeatsAsync(
            booking.ShowId,
            [.. booking.BookingSeats.Select(x => x.SeatId)]
        );

        booking.Status = "Cancelled";

        foreach (var row in rows)
        {
            row.Status = "Available";
        }

        await repo.SaveChangesAsync();

        return (true, null);
    }
}
