using MovieTicketBooking.Models;
namespace MovieTicketBooking.Repositories.Interfaces;

public interface IMovieRepository
{
    Task<List<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(int id);
    Task AddAsync(Movie item);
    Task SaveChangesAsync();
    void Delete(Movie item);
}
