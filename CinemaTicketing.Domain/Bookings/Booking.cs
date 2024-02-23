using CinemaTicketing.Domain.Theaters;

namespace CinemaTicketing.Domain.Booking;

public class Booking
{
    public int Id { get; set; }
    public bool CheckedOut { get; set; }

    public List<Seat> Seats { get; set; } = new();
}