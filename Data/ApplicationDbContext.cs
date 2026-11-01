using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Theatre> Theatres => Set<Theatre>();
    public DbSet<Screen> Screens => Set<Screen>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Show> Shows => Set<Show>();
    public DbSet<ShowSeat> ShowSeats => Set<ShowSeat>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingSeat> BookingSeats => Set<BookingSeat>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Screen - Theatre relationship
        modelBuilder.Entity<Screen>()
            .HasOne(x => x.Theatre)
            .WithMany(x => x.Screens)
            .HasForeignKey(x => x.TheatreId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Screen>()
            .HasIndex(x => new { x.TheatreId, x.ScreenName })
            .IsUnique();

        // Seat - Screen relationship
        modelBuilder.Entity<Seat>()
            .HasOne(x => x.Screen)
            .WithMany(x => x.Seats)
            .HasForeignKey(x => x.ScreenId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Seat>()
            .HasIndex(x => new { x.ScreenId, x.RowName, x.SeatNumber })
            .IsUnique();

        // Show - Movie relationship
        modelBuilder.Entity<Show>()
            .HasOne(x => x.Movie)
            .WithMany(x => x.Shows)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        // Show - Screen relationship
        modelBuilder.Entity<Show>()
            .HasOne(x => x.Screen)
            .WithMany(x => x.Shows)
            .HasForeignKey(x => x.ScreenId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Show>()
            .Property(x => x.TicketPrice)
            .HasPrecision(10, 2);

        // ShowSeat - Show relationship
        modelBuilder.Entity<ShowSeat>()
            .HasOne(x => x.Show)
            .WithMany(x => x.ShowSeats)
            .HasForeignKey(x => x.ShowId)
            .OnDelete(DeleteBehavior.Cascade);

        // ShowSeat - Seat relationship
        modelBuilder.Entity<ShowSeat>()
            .HasOne(x => x.Seat)
            .WithMany(x => x.ShowSeats)
            .HasForeignKey(x => x.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ShowSeat>()
            .HasIndex(x => new { x.ShowId, x.SeatId })
            .IsUnique();

        // Booking - Show relationship
        modelBuilder.Entity<Booking>()
            .HasOne(x => x.Show)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.ShowId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasIndex(x => x.BookingReference)
            .IsUnique();

        modelBuilder.Entity<Booking>()
            .Property(x => x.TotalAmount)
            .HasPrecision(10, 2);

        // BookingSeat - Booking relationship
        modelBuilder.Entity<BookingSeat>()
            .HasOne(x => x.Booking)
            .WithMany(x => x.BookingSeats)
            .HasForeignKey(x => x.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        // BookingSeat - Seat relationship
        modelBuilder.Entity<BookingSeat>()
            .HasOne(x => x.Seat)
            .WithMany(x => x.BookingSeats)
            .HasForeignKey(x => x.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BookingSeat>()
            .Property(x => x.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<User>()
        .HasIndex(x => x.Email)
        .IsUnique();

        // Booking - User relationship
        modelBuilder.Entity<Booking>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
