using CinemaTicketing.Application.Movies.Commands;
using CinemaTicketing.Contracts.Movies.Request;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Application.Movies.Mapping;

public static class MovieContractMapping
{
    public static CreateMovieCommand MapToCreateMovieCommand(this CreateMovieRequest request)
    {
        return new CreateMovieCommand(
            request.Title,
            request.Description,
            request.YearOfRelease,
            request.Director,
            request.Duration,
            request.AgeRestriction,
            request.Genres
        );
    }

    public static UpdateMovieCommand MapToUpdateMovieCommand(this UpdateMovieRequest request, int id)
    {
        return new UpdateMovieCommand(
            id,
            request.Title,
            request.Description,
            request.YearOfRelease,
            request.Director,
            request.Duration,
            request.AgeRestriction,
            request.Genres
        );
    }

    public static Movie MapToMovie(this UpdateMovieCommand request)
    {
        return new Movie
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            YearOfRelease = request.YearOfRelease,
            Director = request.Director,
            Duration = request.Duration,
            AgeRestriction = request.AgeRestriction,
            Genres = request.Genres.Select(g => new Genre(g)).ToList()
        };
    }

    public static MovieResponse MapToMovieResponse(this Movie movie)
    {
        return new MovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            Slug = movie.Slug,
            Description = movie.Description,
            YearOfRelease = movie.YearOfRelease,
            Director = movie.Director,
            Duration = movie.Duration,
            AgeRestriction = movie.AgeRestriction,
            Genres = movie.Genres.Select(x => x.GenreName).ToList()!
        };
    }

    public static List<MovieResponse> MapToListMovieResponse(this IEnumerable<Movie> movies)
    {
        return movies.Select(x => x.MapToMovieResponse()).ToList();
    }
}