using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Contracts.Movies.Request;

public class CreateMovieRequest
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required int YearOfRelease { get; init; }
    public required string Director { get; init; }
    public required int Duration { get; init; }
    public required int AgeRestriction { get; init; }

    public required List<Genre> Genres { get; init; } = [];
}