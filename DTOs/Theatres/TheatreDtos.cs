using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Theatres;

public class CreateTheatreDto
{
    [Required]
    public string? Name
    {
        get; set;
    }

    [Required]
    public string? Location
    {
        get; set;
    }
}

public class UpdateTheatreDto : CreateTheatreDto
{
}

public record TheatreDto(
    int TheatreId,
    string Name,
    string Location
);
