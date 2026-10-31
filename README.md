# Movie Ticket Booking System – Version 1

Complete learning backend baseline built with .NET 10, ASP.NET Core Web API controllers, EF Core 10 and SQL Server.

## Included
- Eight entities and EF Core relationships
- Movies, Theatres, Screens, Seats, Shows, ShowSeats and Bookings workflows
- DTOs, repository interfaces/implementations, services and controllers
- Automatic ShowSeat creation when a show is scheduled
- Booking and cancellation workflows using database transactions
- Scalar OpenAPI UI

## Intentionally not included
Authentication, JWT, authorization, roles/policies, custom middleware, payment processing and advanced concurrency/seat holds. These are intended learning extensions.

## Setup
1. Open the `.csproj` in Visual Studio 2026.
2. Change `DefaultConnection` in `appsettings.json` to your SQL Server instance.
3. Run:

```bash
dotnet restore
dotnet tool install --global dotnet-ef --version 10.0.2
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

4. Open `/scalar/v1` while the app is running in Development.

## Suggested test sequence
1. Create a movie.
2. Create a theatre.
3. Create screens for that theatre.
4. Create physical seats for each screen (single or bulk).
5. Create a show for a movie on a screen. The API creates one ShowSeat row per physical seat.
6. Query `/api/shows/{showId}/seats/available`.
7. Create a booking using the selected physical `seatIds`.
8. Query the seats again; booked seats should show `Booked`.
9. Cancel the booking; its seats return to `Available`.

## Booking safety note
Booking writes are wrapped in a database transaction so a multi-seat booking is all-or-nothing. This starter does not yet implement atomic conditional seat claims or rowversion concurrency. Two simultaneous requests can still race. Add that as a later learning exercise; do not treat this sample as production-ready.

## Architecture
`Controller → Service → Repository → ApplicationDbContext`

There is no authentication or authorization by design. Once the baseline runs, add user registration/login, password hashing, JWT bearer authentication, roles/policies and middleware as separate phases.
