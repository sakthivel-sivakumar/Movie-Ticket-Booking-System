using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories;

public class ShowRepository(ApplicationDbContext db) : IShowRepository
{
    private IQueryable<Show> Details()
    {
        return db.Shows
            .Include(x => x.Movie)
            .Include(x => x.Screen)
            .ThenInclude(x => x!.Theatre);
    }

    public Task<List<Show>> GetAllAsync(
        DateTime? date,
        int? movieId,
        int? theatreId)
    {
        var q = Details().AsNoTracking();

        if (date.HasValue)
        {
            q = q.Where(x => x.StartTime.Date == date.Value.Date);
        }

        if (movieId.HasValue)
        {
            q = q.Where(x => x.MovieId == movieId);
        }

        if (theatreId.HasValue)
        {
            q = q.Where(x => x.Screen!.TheatreId == theatreId);
        }

        return q
            .OrderBy(x => x.StartTime)
            .ToListAsync();
    }

    public Task<Show?> GetByIdAsync(int id)
    {
        return Details()
            .FirstOrDefaultAsync(x => x.ShowId == id);
    }

    public Task AddAsync(Show show)
    {
        return db.Shows
            .AddAsync(show)
            .AsTask();
    }

    public Task SaveChangesAsync()
    {
        return db.SaveChangesAsync();
    }

    public void Delete(Show show)
    {
        db.Shows.Remove(show);
    }

    public Task<List<ShowSeat>> GetShowSeatsAsync(int showId, string? status)
    {
        var q = db.ShowSeats
            .Include(x => x.Seat)
            .Where(x => x.ShowId == showId);

        if (!string.IsNullOrWhiteSpace(status))
        {
            q = q.Where(x => x.Status == status);
        }

        return q
            .OrderBy(x => x.Seat!.RowName)
            .ThenBy(x => x.Seat!.SeatNumber)
            .ToListAsync();
    }

    public Task AddShowSeatsAsync(IEnumerable<ShowSeat> showSeats)
    {
        return db.ShowSeats.AddRangeAsync(showSeats);
    }
}
