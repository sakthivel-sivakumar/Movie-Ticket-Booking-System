namespace MovieTicketBooking.DTOs.Shows;

public class CreateShowDto
{
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

    public decimal TicketPrice
    {
        get; set;
    }
}

public class UpdateShowDto : CreateShowDto
{
}

public record ShowDto(
    int ShowId,
    int MovieId,
    int ScreenId,
    DateTime StartTime,
    DateTime EndTime,
    decimal TicketPrice
);
