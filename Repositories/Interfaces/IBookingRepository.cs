using MovieTicketBooking.Models;
namespace MovieTicketBooking.Repositories.Interfaces;

public interface IBookingRepository
{
    Task<List<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(int id);

    Task<Booking?> GetByIdForUpdateAsync(int id);
    Task<List<Booking>> GetByShowAsync(int showId);
    Task<Show?> GetShowAsync(int showId);
    Task<List<ShowSeat>> GetShowSeatsAsync(int showId, List<int> seatIds);
    Task SaveChangesAsync();
    Task<Booking> SaveBookingAsync(Booking booking, List<ShowSeat> showSeats);
}
