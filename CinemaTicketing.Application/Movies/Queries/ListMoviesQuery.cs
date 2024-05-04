using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Movies;
using ErrorOr;
using MediatR;

namespace CinemaTicketing.Application.Movies.Queries;

public record ListMoviesQuery : IRequest<ErrorOr<List<Movie>>>;

public class ListmoviesQueryHandler : IRequestHandler<ListMoviesQuery, ErrorOr<List<Movie>>>
{
    private readonly IMovieRepository _movieRepository;

    public ListmoviesQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<ErrorOr<List<Movie>>> Handle(ListMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _movieRepository.ListAsync(cancellationToken);
    }
}