using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Movies;
using ErrorOr;
using MediatR;

namespace CinemaTicketing.Application.Movies.Queries;

public record GetMovieByIdQuery(int Id) : IRequest<ErrorOr<Movie?>>;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, ErrorOr<Movie?>>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<ErrorOr<Movie?>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        return await _movieRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}