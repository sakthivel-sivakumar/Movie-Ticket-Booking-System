using MovieTicketBooking.DTOs.Shows;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;
namespace MovieTicketBooking.Services;

public class ShowService(IShowRepository repo, ISeatRepository seatRepo) : IShowService
{
    private static ShowDto Map(Show x) => new(x.ShowId, x.MovieId, x.ScreenId, x.StartTime, x.EndTime, x.TicketPrice);
    public async Task<List<ShowDto>> GetAllAsync(DateTime? date, int? movieId, int? theatreId) => [.. (await repo.GetAllAsync(date, movieId, theatreId)).Select(Map)];
    public async Task<ShowDto?> GetByIdAsync(int id)
    {
        var x = await repo.GetByIdAsync(id);
        return x is null ? null : Map(x);
    }
    public async Task<ShowDto?> CreateAsync(CreateShowDto dto)
    {
        if (dto.EndTime <= dto.StartTime || dto.TicketPrice < 0)
            return null;
        var physicalSeats = await seatRepo.GetAllAsync();
        var screenSeats = physicalSeats.Where(x => x.ScreenId == dto.ScreenId).ToList();
        var show = new Show { MovieId = dto.MovieId, ScreenId = dto.ScreenId, StartTime = dto.StartTime, EndTime = dto.EndTime, TicketPrice = dto.TicketPrice };
        await repo.AddAsync(show);
        await repo.SaveChangesAsync();
        await repo.AddShowSeatsAsync(screenSeats.Select(s => new ShowSeat { ShowId = show.ShowId, SeatId = s.SeatId, Status = "Available" }));
        await repo.SaveChangesAsync();
        return await GetByIdAsync(show.ShowId);
    }
    public async Task<ShowDto?> UpdateAsync(int id, UpdateShowDto dto)
    {
        var x = await repo.GetByIdAsync(id);
        if (x is null || dto.EndTime <= dto.StartTime)
            return null;
        x.MovieId = dto.MovieId;
        x.ScreenId = dto.ScreenId;
        x.StartTime = dto.StartTime;
        x.EndTime = dto.EndTime;
        x.TicketPrice = dto.TicketPrice;
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
    public async Task<List<ShowSeatDto>?> GetSeatsAsync(int showId, string? status)
    {
        var rows = await repo.GetShowSeatsAsync(showId, status);
        if (rows.Count == 0)
            return [];
        return [.. rows.Select(x => new ShowSeatDto(x.ShowSeatId, x.SeatId, x.Seat!.RowName, x.Seat.SeatNumber, x.Seat.SeatType, x.Status))];
    }
}
