using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Movies;
using ErrorOr;
using MediatR;

namespace CinemaTicketing.Application.Movies.Queries;

public record GetGenreByIdQuery(int Id) : IRequest<ErrorOr<Genre?>>;

public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, ErrorOr<Genre?>>
{
    private readonly IGenreRepository _genreRepository;

    public GetGenreByIdQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<ErrorOr<Genre?>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        return await _genreRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}