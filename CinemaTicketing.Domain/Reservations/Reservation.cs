using CinemaTicketing.Domain.Screenings;

namespace CinemaTicketing.Domain.Reservations;

public class Reservation
{
    public int Id { get; set; }
    public bool Confirmed { get; set; }
    public DateTime ReservedUntil { get; set; }

    public Guid UserId { get; init; }
    public int ScreeningId { get; init; }

    public User User { get; init; }
    public Screening Screening { get; set; } = null!;
    public List<SeatReservation> SeatReservations { get; set; } = new();
}