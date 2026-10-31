using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories;

public class TheatreRepository(ApplicationDbContext db) : ITheatreRepository
{
    public Task<List<Theatre>> GetAllAsync()
    {
        return db.Theatres
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<Theatre?> GetByIdAsync(int id)
    {
        return db.Theatres
            .FindAsync(id)
            .AsTask();
    }

    public Task AddAsync(Theatre item)
    {
        return db.Theatres
            .AddAsync(item)
            .AsTask();
    }

    public Task SaveChangesAsync()
    {
        return db.SaveChangesAsync();
    }

    public void Delete(Theatre item)
    {
        db.Theatres.Remove(item);
    }
}
