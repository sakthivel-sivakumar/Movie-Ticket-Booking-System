using System.ComponentModel.DataAnnotations;
namespace MovieTicketBooking.DTOs.Seats;

public class CreateSeatDto
{
    public int ScreenId
    {
        get; set;
    }
    public string? RowName
    {
        get; set;
    }
    public int SeatNumber
    {
        get; set;
    }
    public string? SeatType
    {
        get; set;
    }
}
public class UpdateSeatDto : CreateSeatDto
{
}
public record SeatDto(int SeatId, int ScreenId, string RowName, int SeatNumber, string SeatType);
