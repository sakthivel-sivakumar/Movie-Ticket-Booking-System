using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Bookings;

public class CreateBookingDto
{
    [Required]
    [MaxLength(15)]
    public string CustomerPhone { get; set; } = string.Empty;

    public int ShowId
    {
        get; set;
    }

    public List<int> SeatIds { get; set; } = [];
}

public record BookingSeatDto(
    int SeatId,
    string RowName,
    int SeatNumber,
    decimal Price
);

public record BookingDto(
    int BookingId,
    string BookingReference,
    int ShowId,
    string CustomerName,
    string CustomerEmail,
    string CustomerPhone,
    DateTime BookingDate,
    decimal TotalAmount,
    string Status,
    List<BookingSeatDto> Seats
);
