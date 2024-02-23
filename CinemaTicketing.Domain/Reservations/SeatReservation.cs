using CinemaTicketing.Domain.Theaters;

namespace CinemaTicketing.Domain.Reservations;

public class SeatReservation
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public int SeatId { get; set; }

    public Reservation Reservation { get; set; } = null!;
    public Seat Seat { get; set; } = null!;
}