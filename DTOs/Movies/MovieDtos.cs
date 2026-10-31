using System.ComponentModel.DataAnnotations;

namespace MovieTicketBooking.DTOs.Movies;

public class CreateMovieDto
{
    public string? Title
    {
        get; set;
    }

    public string? Description
    {
        get; set;
    }

    public int DurationMinutes
    {
        get; set;
    }

    public DateOnly ReleaseDate
    {
        get; set;
    }

    public string? Language
    {
        get; set;
    }

    public string? Genre
    {
        get; set;
    }
}

public class UpdateMovieDto : CreateMovieDto
{

}

public record MovieDto(
    int MovieId,
    string Title,
    string? Description,
    int DurationMinutes,
    DateOnly ReleaseDate,
    string Language,
    string Genre
);
