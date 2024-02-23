using CinemaTicketing.Domain.Reservations;

namespace CinemaTicketing.Domain;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateOnly BirthDay { get; set; }

    public string Password { get; set; }

    public List<Reservation> Reservations { get; set; } = new();
}