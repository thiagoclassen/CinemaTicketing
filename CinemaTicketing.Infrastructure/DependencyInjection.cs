using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Infrastructure.Common;
using CinemaTicketing.Infrastructure.Movies.Persistence;
using CinemaTicketing.Infrastructure.Reservations.Persistence;
using CinemaTicketing.Infrastructure.Screenings.Persistence;
using CinemaTicketing.Infrastructure.Theaters.Persistence;
using CinemaTicketing.Infrastructure.Users.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaTicketing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddPersistence();
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();

        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ISeatReservationRepository, SeatReservationRepository>();

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();

        services.AddScoped<IScreeningRepository, ScreeningRepository>();

        services.AddScoped<ITheaterRepository, TheaterRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}