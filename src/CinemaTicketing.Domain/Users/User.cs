using CinemaTicketing.Domain.Reservations;

namespace CinemaTicketing.Domain.Users;

public sealed class User
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required DateOnly BirthDay { get; init; }

    public required string Password { get; init; }

    public List<Reservation> Reservations { get; init; } = [];
}