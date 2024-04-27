using CinemaTicketing.Domain.Reservations;

namespace CinemaTicketing.Domain.Users;

public class User
{
    public Guid Id { get; set; }

    public required string Name { get; init; }

    public required DateOnly BirthDay { get; init; }

    public required string Password { get; init; }

    public List<Reservation> Reservations { get; init; } = [];
}