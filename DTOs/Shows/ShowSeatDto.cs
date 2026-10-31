namespace MovieTicketBooking.DTOs.Shows;

public record ShowSeatDto(
    int ShowSeatId,
    int SeatId,
    string RowName,
    int SeatNumber,
    string SeatType,
    string Status
);
