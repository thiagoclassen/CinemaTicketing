using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Movies;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Movies.Queries;

public record GetMovieByIdQuery(int Id) : IRequest<Movie?>;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie?>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Movie?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        return await _movieRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}

public class GetMovieByIdQueryValidator : AbstractValidator<GetMovieByIdQuery>
{
    public GetMovieByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}