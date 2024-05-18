using CinemaTicketing.Application.Movies.Repositories;
using CinemaTicketing.Domain.Movies;
using ErrorOr;
using MediatR;

namespace CinemaTicketing.Application.Movies.Queries;

public record ListGenresQuery : IRequest<ErrorOr<List<Genre>>>;

public class ListGenresQueryHandler : IRequestHandler<ListGenresQuery, ErrorOr<List<Genre>>>
{
    private readonly IGenreRepository _genreRepository;

    public ListGenresQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ErrorOr<List<Genre>>> Handle(ListGenresQuery request, CancellationToken cancellationToken)
    {
        return await _genreRepository.ListAsync(cancellationToken);
    }
}