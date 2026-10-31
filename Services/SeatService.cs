using MovieTicketBooking.DTOs.Seats;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services;

public class SeatService(ISeatRepository repo) : ISeatService
{
    private static SeatDto Map(Seat x)
    {
        return new SeatDto(
            x.SeatId,
            x.ScreenId,
            x.RowName,
            x.SeatNumber,
            x.SeatType
        );
    }

    public async Task<List<SeatDto>> GetByScreenAsync(int id)
    {
        return [.. (await repo.GetByScreenAsync(id)).Select(Map)];
    }

    public async Task<SeatDto?> GetByIdAsync(int id)
    {
        var x = await repo.GetByIdAsync(id);

        return x is null ? null : Map(x);
    }

    public async Task<SeatDto> CreateAsync(CreateSeatDto d)
    {
        var x = new Seat
        {
            ScreenId = d.ScreenId,
            RowName = d.RowName!,
            SeatNumber = d.SeatNumber,
            SeatType = d.SeatType!
        };

        await repo.AddAsync(x);
        await repo.SaveChangesAsync();

        return Map(x);
    }

    public async Task<List<SeatDto>> CreateBulkAsync(BulkCreateSeatsDto d)
    {
        var rows = d.Seats
            .Select(x => new Seat
            {
                ScreenId = d.ScreenId,
                RowName = x.RowName,
                SeatNumber = x.SeatNumber,
                SeatType = x.SeatType
            })
            .ToList();

        await repo.AddRangeAsync(rows);
        await repo.SaveChangesAsync();

        return [.. rows.Select(Map)];
    }

    public async Task<SeatDto?> UpdateAsync(int id, UpdateSeatDto d)
    {
        var x = await repo.GetByIdAsync(id);

        if (x is null)
            return null;

        x.ScreenId = d.ScreenId;
        x.RowName = d.RowName!;
        x.SeatNumber = d.SeatNumber;
        x.SeatType = d.SeatType!;

        await repo.SaveChangesAsync();

        return Map(x);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var x = await repo.GetByIdAsync(id);

        if (x is null)
            return false;

        repo.Delete(x);
        await repo.SaveChangesAsync();

        return true;
    }
}
