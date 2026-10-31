using MovieTicketBooking.DTOs.Screens;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;
namespace MovieTicketBooking.Services;

public class ScreenService(IScreenRepository repo) : IScreenService
{
    private static ScreenDto Map(Screen x) => new(x.ScreenId, x.TheatreId, x.ScreenName);
    public async Task<List<ScreenDto>> GetAllAsync() => [.. (await repo.GetAllAsync()).Select(Map)];
    public async Task<List<ScreenDto>> GetByTheatreAsync(int theatreId) => [.. (await repo.GetByTheatreAsync(theatreId)).Select(Map)];
    public async Task<ScreenDto?> GetByIdAsync(int id)
    {
        var x = await repo.GetByIdAsync(id);
        return x is null ? null : Map(x);
    }
    public async Task<ScreenDto> CreateAsync(CreateScreenDto dto)
    {
        var x = new Screen { TheatreId = dto.TheatreId, ScreenName = dto.ScreenName! };
        await repo.AddAsync(x);
        await repo.SaveChangesAsync();
        return Map(x);
    }
    public async Task<ScreenDto?> UpdateAsync(int id, UpdateScreenDto dto)
    {
        var x = await repo.GetByIdAsync(id);
        if (x is null)
            return null;
        x.TheatreId = dto.TheatreId;
        x.ScreenName = dto.ScreenName!;
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
