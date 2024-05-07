using System.Text.Json;
using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Contracts.Movies.Request;
using CinemaTicketing.Contracts.Movies.Response;
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
                "Action",
                "SciFi"
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
                "Action",
                "SciFi"
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
                "Comedy",
                "SciFi"
            ]
        };
    }

    public static MovieResponse GetMovieResponse()
    {
        return new MovieResponse
        {
            Id = 1,
            Title = "The Matrix 4",
            Description =
                "A terrible movie...",
            YearOfRelease = 2023,
            Slug = "the-matrix-4-2023",
            Director = "Lana Wachowski, Lilly Wachowski",
            Duration = 150,
            AgeRestriction = 15,
            Genres =
            [
                "Comedy",
                "SciFi"
            ]
        };
    }

    public static Movie GetMovie(CreateMovieRequest createMovieRequest, int id = 1)
    {
        return new Movie
        {
            Id = id,
            Title = createMovieRequest.Title,
            Description = createMovieRequest.Description,
            YearOfRelease = createMovieRequest.YearOfRelease,
            Director = createMovieRequest.Director,
            Duration = createMovieRequest.Duration,
            AgeRestriction = createMovieRequest.AgeRestriction,
            Genres = createMovieRequest.Genres.Select(genre => new Genre(genre)).ToList()
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
            Genres = createMovieCommand.Genres.Select(genre => new Genre(genre)).ToList()
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
                "Action",
                "SciFi"
            ]
        );
    }

    public static List<Movie> ListMovies()
    {
        return
        [
            new Movie
            {
                Id = 1,
                Title = "The Matrix",
                Description =
                    "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.",
                YearOfRelease = 1999,
                Director = "Lana Wachowski, Lilly Wachowski",
                Duration = 150,
                AgeRestriction = 15,
                Genres =
                [
                    new Genre("Action"),
                    new Genre("SciFi")
                ]
            },
            new Movie
            {
                Id = 1,
                Title = "The Matrix 4",
                Description =
                    "A terrible movie...",
                YearOfRelease = 2023,
                Director = "Lana Wachowski, Lilly Wachowski",
                Duration = 150,
                AgeRestriction = 15,
                Genres =
                [
                    new Genre("Action"),
                    new Genre("SciFi")
                ]
            }
        ];
    }

    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}