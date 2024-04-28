using CinemaTicketing.Domain.Movies;
using CinemaTicketing.Domain.Reservations;
using CinemaTicketing.Domain.Screenings;
using CinemaTicketing.Domain.Theaters;
using CinemaTicketing.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CinemaTicketing.Infrastructure.Common;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Theater> Theaters { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<Seat> Seats { get; set; } = null!;
    public DbSet<Screening> Screenings { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;
    public DbSet<SeatReservation> SeatReservations { get; set; } = null!;

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(
                "Server=localhost,1433;Database=CinemaTicketing;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=Yes")
            .LogTo(Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}