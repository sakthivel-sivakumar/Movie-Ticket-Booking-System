namespace MovieTicketBooking.DTOs.Seats;

public class BulkCreateSeatsDto
{
    public int ScreenId
    {
        get; set;
    }
    public List<SeatInputDto> Seats { get; set; } = [];
}
public class SeatInputDto
{
    public string RowName { get; set; } = string.Empty;
    public int SeatNumber
    {
        get; set;
    }
    public string SeatType { get; set; } = "Regular";
}
