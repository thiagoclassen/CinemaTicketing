namespace CinemaTicketing.Domain.Theaters;

public class Theater
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }

    public List<Room> Rooms { get; init; } = [];
}