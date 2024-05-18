using CinemaTicketing.Domain.Screenings;
using CinemaTicketing.Domain.Users;

namespace CinemaTicketing.Domain.Reservations;

public sealed class Reservation
{
    public int Id { get; init; }
    public required bool Confirmed { get; init; }
    public required DateTime ReservedUntil { get; init; }

    public Guid UserId { get; init; }
    public int ScreeningId { get; init; }

    public User User { get; init; } = null!;
    public Screening Screening { get; init; } = null!;
    public List<SeatReservation> SeatReservations { get; set; } = [];
}