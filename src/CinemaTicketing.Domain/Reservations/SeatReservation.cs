using CinemaTicketing.Domain.Theaters;

namespace CinemaTicketing.Domain.Reservations;

public sealed class SeatReservation
{
    public int Id { get; init; }
    public int ReservationId { get; init; }
    public int SeatId { get; init; }

    public Reservation Reservation { get; init; } = null!;
    public Seat Seat { get; init; } = null!;
}