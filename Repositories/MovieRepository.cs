using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories;

public class MovieRepository(ApplicationDbContext db) : IMovieRepository
{
    public Task<List<Movie>> GetAllAsync()
    {
        return db.Movies
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<Movie?> GetByIdAsync(int id)
    {
        return db.Movies
            .FindAsync(id)
            .AsTask();
    }

    public Task AddAsync(Movie item)
    {
        return db.Movies
            .AddAsync(item)
            .AsTask();
    }

    public Task SaveChangesAsync()
    {
        return db.SaveChangesAsync();
    }

    public void Delete(Movie item)
    {
        db.Movies.Remove(item);
    }
}
