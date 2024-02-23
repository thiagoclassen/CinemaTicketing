using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Domain.Screenings;

public class Screening
{
    public int Id { get; set; }
    public DateTime Schedule { get; set; }
    public decimal Price { get; set; }

    public int MovieId { get; set; }
    public int RoomId  { get; set; }
}