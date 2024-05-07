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
    public DbSet<User> Users { get; init; }
    public DbSet<Theater> Theaters { get; init; }
    public DbSet<Room> Rooms { get; init; }
    public DbSet<Seat> Seats { get; init; }
    public DbSet<Screening> Screenings { get; init; }
    public DbSet<Movie> Movies { get; init; }
    public DbSet<Genre> Genres { get; init; }
    public DbSet<Reservation> Reservations { get; init; }
    public DbSet<SeatReservation> SeatReservations { get; init; }

    public AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            // .UseSqlServer(
            //     "Server=localhost,1433;Database=CinemaTicketing;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=Yes")
            .LogTo(Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<Genre>().HasData(Genre.ListGenres());

        base.OnModelCreating(modelBuilder);
    }
}