using System.Text.Json;
using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Contracts.Movies.Request;
using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Tests.Utils.Movies;

public static class MovieConstants
{
    public static CreateMovieRequest GetValidMovieRequest()
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

    public static CreateMovieRequest GetInvalidMovieRequest()
    {
        return new CreateMovieRequest
        {
            Title = "",
            Description =
                "",
            YearOfRelease = 0,
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

    public static UpdateMovieRequest GetUpdateMovieRequest()
    {
        return new UpdateMovieRequest
        {
            Title = "The Matrix 4",
            Description =
                "A terrible movie...",
            YearOfRelease = 2023,
            Director = "Lana Wachowski, Lilly Wachowski",
            Duration = 150,
            AgeRestriction = 15,
            Genres =
            [
                new Genre { GenreName = "Action" },
                new Genre { GenreName = "Sci-Fi" },
                new Genre { GenreName = "Comedy" }
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

    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}