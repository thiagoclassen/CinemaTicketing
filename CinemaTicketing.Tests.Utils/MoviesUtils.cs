using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Contracts.Movies;
using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Tests.Utils;

public static class MoviesUtils
{
    public static CreateMovieRequest GetMovieRequest()
    {
        return new CreateMovieRequest
        {
            Title = "The Matrix",
            Description =
                "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
            YearOfRelease = 1999,
            Director = "Lana Wachowski, Lilly Wachowski",
            Duration = 150,
            AgeRestriction = 15,
            Genres =
            [
                new Genre { GenreName = "Action" },
                new Genre { GenreName = "Sci-Fi" }
            ]
        };
    }

    public static Movie GetMovie(CreateMovieRequest createMovieRequest)
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

    public static Movie GetMovie(CreateMovieCommand createMovieCommand)
    {
        return new Movie
        {
            Id = 0,
            Title = createMovieCommand.Title,
            Description = createMovieCommand.Description,
            YearOfRelease = createMovieCommand.YearOfRelease,
            Director = createMovieCommand.Director,
            Duration = createMovieCommand.Duration,
            AgeRestriction = createMovieCommand.AgeRestriction,
            Genres = createMovieCommand.Genres
        };
    }

    public static CreateMovieCommand GetMovieCommand()
    {
        return new CreateMovieCommand(
            "The Matrix",
            "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
            1999,
            "Lana Wachowski, Lilly Wachowski",
            120,
            15,
            [
                new Genre { GenreName = "Action" },
                new Genre { GenreName = "Sci-Fi" }
            ]
        );
    }
}