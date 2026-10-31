using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories;

public class SeatRepository(ApplicationDbContext db) : ISeatRepository
{
    public Task<List<Seat>> GetAllAsync()
    {
        return db.Seats
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<List<Seat>> GetByScreenAsync(int screenId)
    {
        return db.Seats
            .AsNoTracking()
            .Where(x => x.ScreenId == screenId)
            .OrderBy(x => x.RowName)
            .ThenBy(x => x.SeatNumber)
            .ToListAsync();
    }

    public Task<Seat?> GetByIdAsync(int id)
    {
        return db.Seats
            .FindAsync(id)
            .AsTask();
    }

    public Task AddAsync(Seat item)
    {
        return db.Seats
            .AddAsync(item)
            .AsTask();
    }

    public Task AddRangeAsync(IEnumerable<Seat> items)
    {
        return db.Seats.AddRangeAsync(items);
    }

    public Task SaveChangesAsync()
    {
        return db.SaveChangesAsync();
    }

    public void Delete(Seat item)
    {
        db.Seats.Remove(item);
    }
}
