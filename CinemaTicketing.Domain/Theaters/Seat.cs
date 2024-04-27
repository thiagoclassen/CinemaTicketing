namespace CinemaTicketing.Domain.Theaters;

public class Seat
{
    public int Id { get; init; }
    public required string Row { get; init; }
    public required int Column { get; init; }

    public required int RoomId { get; init; }
}