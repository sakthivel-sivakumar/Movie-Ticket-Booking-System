using MovieTicketBooking.DTOs.Auth;

namespace MovieTicketBooking.Services.Interfaces;

public interface IUserService
{
    Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto);
}

