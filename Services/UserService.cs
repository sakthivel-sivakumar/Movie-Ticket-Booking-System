using Microsoft.AspNetCore.Identity;
using MovieTicketBooking.DTOs.Auth;
using MovieTicketBooking.Models;
using MovieTicketBooking.Repositories.Interfaces;
using MovieTicketBooking.Services.Interfaces;

namespace MovieTicketBooking.Services;

public class UserService(
    IUserRepository repo,
    IPasswordHasher<User> passwordHasher) : IUserService
{
    public async Task<(bool Success, string Message)> RegisterAsync(
        RegisterDto dto)
    {
        var email = dto.Email.Trim().ToLowerInvariant();

        var existingUser = await repo.GetByEmailAsync(email);

        if (existingUser is not null)
            return (false, "Email is already registered.");

        var user = new User
        {
            FullName = dto.FullName,
            Email = email,
            Role = "Customer"
        };

        user.PasswordHash = passwordHasher.HashPassword(
            user,
            dto.Password
        );

        await repo.AddAsync(user);
        await repo.SaveChangesAsync();

        return (true, "Registration successful.");
    }
}
