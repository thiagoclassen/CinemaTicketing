using CinemaTicketing.Domain.Theaters;

namespace CinemaTicketing.Domain.Booking;

public class Reservation
{
    public int Id { get; set; }
    public bool Confirmed { get; set; }
    public DateTime ReservedUntil { get; set; }

    public Guid UserId { get; init; }
    public User User { get; init; }
    public List<Seat> Seats { get; set; } = new();
}