namespace CinemaTicketing.Domain.Movies;

public class Genre
{
    public Genre()
    {
    }

    public Genre(string genre)
    {
        GenreName = genre;
    }

    public int Id { get; init; }
    public required string GenreName { get; init; } 
}