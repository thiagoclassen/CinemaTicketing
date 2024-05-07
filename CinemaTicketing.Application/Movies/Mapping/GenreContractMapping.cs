using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Application.Movies.Mapping;

public static class GenreContractMapping
{
    public static GenreResponse MapToGenreResponse(this Genre genre)
    {
        return new GenreResponse
        {
            Id = genre.Id,
            GenreName = genre.GenreName
        };
    }

    public static List<GenreResponse> MapToListGenresResponse(this IEnumerable<Genre> genres)
    {
        return genres.Select(g => new GenreResponse
        {
            Id = g.Id,
            GenreName = g.GenreName
        }).ToList();
    }
}