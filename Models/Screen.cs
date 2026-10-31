using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.Models;

public class Screen
{
    public int ScreenId
    {
        get; set;
    }

    public int TheatreId
    {
        get; set;
    }

    [Required]
    [MaxLength(50)]
    public string ScreenName { get; set; } = string.Empty;

    public Theatre? Theatre
    {
        get; set;
    }

    public ICollection<Seat> Seats { get; set; } = [];

    public ICollection<Show> Shows { get; set; } = [];
}
