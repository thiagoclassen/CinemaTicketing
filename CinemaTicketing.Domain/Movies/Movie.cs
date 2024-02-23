namespace CinemaTicketing.Domain.Movies;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public string Director { get; set; }
    public int AgeRestriction { get; set; }

    public List<Genre> Genres { get; set; } = new();
}