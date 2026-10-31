using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.Models;

public class Seat
{
    public int SeatId
    {
        get; set;
    }

    public int ScreenId
    {
        get; set;
    }

    [Required]
    [MaxLength(5)]
    public string RowName { get; set; } = string.Empty;

    [Range(1, 500)]
    public int SeatNumber
    {
        get; set;
    }

    [Required]
    [MaxLength(30)]
    public string SeatType { get; set; } = "Regular";

    public Screen? Screen
    {
        get; set;
    }

    public ICollection<ShowSeat> ShowSeats { get; set; } = [];

    public ICollection<BookingSeat> BookingSeats { get; set; } = [];
}
