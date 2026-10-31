using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories;

public class ScreenRepository(ApplicationDbContext db) : IScreenRepository
{
    public Task<List<Screen>> GetAllAsync()
    {
        return db.Screens
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<List<Screen>> GetByTheatreAsync(int theatreId)
    {
        return db.Screens
            .AsNoTracking()
            .Where(x => x.TheatreId == theatreId)
            .ToListAsync();
    }

    public Task<Screen?> GetByIdAsync(int id)
    {
        return db.Screens
            .FindAsync(id)
            .AsTask();
    }

    public Task AddAsync(Screen item)
    {
        return db.Screens
            .AddAsync(item)
            .AsTask();
    }

    public Task SaveChangesAsync()
    {
        return db.SaveChangesAsync();
    }

    public void Delete(Screen item)
    {
        db.Screens.Remove(item);
    }
}
