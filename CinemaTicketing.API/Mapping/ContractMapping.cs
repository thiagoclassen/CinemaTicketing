using CinemaTicketing.Contracts.Movies;
using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.API.Mapping;

public static class ContractMapping
{
    public static Movie MapToMovie(this CreateMovieRequest request)
    {
        return new Movie
        {
            Title = request.Title,
            Description = request.Description,
            YearOfRelease = request.YearOfRelease,
            Director = request.Director,
            Duration = request.Duration,
            AgeRestriction = request.AgeRestriction
        };
    }
}