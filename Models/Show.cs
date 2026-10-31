using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.Models;

public class Show
{
    public int ShowId
    {
        get; set;
    }

    public int MovieId
    {
        get; set;
    }

    public int ScreenId
    {
        get; set;
    }

    public DateTime StartTime
    {
        get; set;
    }

    public DateTime EndTime
    {
        get; set;
    }

    [Range(typeof(decimal), "0", "999999")]
    public decimal TicketPrice
    {
        get; set;
    }

    public Movie? Movie
    {
        get; set;
    }

    public Screen? Screen
    {
        get; set;
    }

    public ICollection<ShowSeat> ShowSeats { get; set; } = [];

    public ICollection<Booking> Bookings { get; set; } = [];
}
