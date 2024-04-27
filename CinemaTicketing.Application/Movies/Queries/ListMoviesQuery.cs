using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Movies;
using MediatR;

namespace CinemaTicketing.Application.Movies.Queries;

// TODO - Double check this...
public record ListMoviesQuery : IRequest<List<Movie>>;

public class ListmoviesQueryHandler : IRequestHandler<ListMoviesQuery, List<Movie>>
{
    private readonly IMovieRepository _movieRepository;

    public ListmoviesQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<List<Movie>> Handle(ListMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _movieRepository.ListAsync(cancellationToken);
    }
}