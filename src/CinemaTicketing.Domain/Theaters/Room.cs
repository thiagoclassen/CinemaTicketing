using CinemaTicketing.Domain.Screenings;

namespace CinemaTicketing.Domain.Theaters;

public sealed class Room
{
    public int Id { get; set; }
    public int Capacity => RowsCapacity * ColumnsCapacity;
    public int RowsCapacity { get; set; }
    public int ColumnsCapacity { get; set; }

    public int TheaterId { get; set; }
    public List<Seat> Seats { get; set; } = [];
    public List<Screening> Screenings { get; set; } = [];
}