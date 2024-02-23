namespace CinemaTicketing.Domain.Theaters;

public class Seat
{
    public int Id { get; set; }
    public string Row { get; set; }
    public int Column { get; set; }
    
    public int RoomId { get; set; }
}