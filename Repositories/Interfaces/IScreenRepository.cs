using MovieTicketBooking.Models;
namespace MovieTicketBooking.Repositories.Interfaces;

public interface IScreenRepository
{
    Task<List<Screen>> GetAllAsync();
    Task<List<Screen>> GetByTheatreAsync(int theatreId);
    Task<Screen?> GetByIdAsync(int id);
    Task AddAsync(Screen item);
    Task SaveChangesAsync();
    void Delete(Screen item);
}
