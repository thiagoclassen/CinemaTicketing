namespace CinemaTicketing.Contracts.Movies.Response;

public class MovieResponse
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required string Slug { get; init; }
    public required string Description { get; init; }
    public required int YearOfRelease { get; init; }
    public required string Director { get; init; }
    public required int Duration { get; init; }
    public required int AgeRestriction { get; init; }

    public required IEnumerable<string> Genres { get; init; } = Enumerable.Empty<string>();
}