using MovieTicketBooking.Models;
namespace MovieTicketBooking.Repositories.Interfaces;

public interface ITheatreRepository
{
    Task<List<Theatre>> GetAllAsync();
    Task<Theatre?> GetByIdAsync(int id);
    Task AddAsync(Theatre item);
    Task SaveChangesAsync();
    void Delete(Theatre item);
}
