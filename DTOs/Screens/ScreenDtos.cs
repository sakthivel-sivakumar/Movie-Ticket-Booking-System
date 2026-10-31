using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Screens;

public class CreateScreenDto
{
    public int TheatreId
    {
        get; set;
    }

    public string? ScreenName
    {
        get; set;
    }
}

public class UpdateScreenDto : CreateScreenDto
{
}

public record ScreenDto(
    int ScreenId,
    int TheatreId,
    string ScreenName
);
