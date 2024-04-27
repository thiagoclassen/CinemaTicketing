using CinemaTicketing.Contracts.Movies;
using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Api.Tests.Unit.Utils;

internal static class MoviesUtils
{
    internal static CreateMovieRequest GetMovieRequest()
    {
        return new CreateMovieRequest
        {
            Title = "The Matrix",
            Description =
                "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
            YearOfRelease = 1999,
            Director = "Lana Wachowski, Lilly Wachowski",
            Duration = 0,
            AgeRestriction = 15,
            Genres =
            [
                new Genre { GenreName = "Action" },
                new Genre { GenreName = "Sci-Fi" }
            ]
        };
    }

    internal static Movie GetMovie(CreateMovieRequest createMovieRequest)
    {
        return new Movie
        {
            Id = 1,
            Title = createMovieRequest.Title,
            Description = createMovieRequest.Description,
            YearOfRelease = createMovieRequest.YearOfRelease,
            Director = createMovieRequest.Director,
            Duration = createMovieRequest.Duration,
            AgeRestriction = createMovieRequest.AgeRestriction,
            Genres = createMovieRequest.Genres
        };
    }
}