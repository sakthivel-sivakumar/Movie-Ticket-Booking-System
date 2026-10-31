using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MovieTicketBooking.Data;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;

namespace MovieTicketBooking.Repositories;

public class BookingRepository(ApplicationDbContext db) : IBookingRepository
{
    private IQueryable<Booking> Details()
    {
        return db.Bookings
            .Include(x => x.BookingSeats)
            .ThenInclude(x => x.Seat);
    }

    public Task<List<Booking>> GetAllAsync()
    {
        return Details()
            .AsNoTracking()
            .OrderByDescending(x => x.BookingDate)
            .ToListAsync();
    }

    public Task<Booking?> GetByIdAsync(int id)
    {
        return Details()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BookingId == id);
    }

    public Task<List<Booking>> GetByShowAsync(int showId)
    {
        return Details()
            .AsNoTracking()
            .Where(x => x.ShowId == showId)
            .ToListAsync();
    }

    public Task<Show?> GetShowAsync(int showId)
    {
        return db.Shows
            .Include(x => x.Screen)
            .ThenInclude(x => x!.Seats)
            .FirstOrDefaultAsync(x => x.ShowId == showId);
    }

    public Task<List<ShowSeat>> GetShowSeatsAsync(int showId, List<int> seatIds)
    {
        return db.ShowSeats
            .Include(x => x.Seat)
            .Where(x => x.ShowId == showId && seatIds.Contains(x.SeatId))
            .ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        return db.SaveChangesAsync();
    }

    public async Task<Booking> SaveBookingAsync(
        Booking booking,
        List<ShowSeat> showSeats)
    {
        await using IDbContextTransaction tx =
            await db.Database.BeginTransactionAsync();

        try
        {
            db.Bookings.Add(booking);

            foreach (var row in showSeats)
            {
                row.Status = "Booked";
            }

            await db.SaveChangesAsync();
            await tx.CommitAsync();

            return booking;
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }
}
