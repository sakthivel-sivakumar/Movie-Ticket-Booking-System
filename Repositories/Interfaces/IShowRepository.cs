using MovieTicketBooking.Models;
namespace MovieTicketBooking.Repositories.Interfaces;

public interface IShowRepository
{
    Task<List<Show>> GetAllAsync(DateTime? date, int? movieId, int? theatreId);
    Task<Show?> GetByIdAsync(int id);
    Task AddAsync(Show show);
    Task SaveChangesAsync();
    void Delete(Show show);
    Task<List<ShowSeat>> GetShowSeatsAsync(int showId, string? status);
    Task AddShowSeatsAsync(IEnumerable<ShowSeat> showSeats);
}
