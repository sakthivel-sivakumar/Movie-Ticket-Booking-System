using MovieTicketBooking.Models;
namespace MovieTicketBooking.Repositories.Interfaces;

public interface ISeatRepository
{
    Task<List<Seat>> GetAllAsync();
    Task<List<Seat>> GetByScreenAsync(int screenId);
    Task<Seat?> GetByIdAsync(int id);
    Task AddAsync(Seat item);
    Task AddRangeAsync(IEnumerable<Seat> items);
    Task SaveChangesAsync();
    void Delete(Seat item);
}
