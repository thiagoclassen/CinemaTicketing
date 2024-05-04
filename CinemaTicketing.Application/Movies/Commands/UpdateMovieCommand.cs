using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Application.Movies.Mapping;
using CinemaTicketing.Contracts.Movies.Response;
using CinemaTicketing.Domain.Common.Errors;
using CinemaTicketing.Domain.Movies;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Movies.Commands;

public record UpdateMovieCommand(
    int Id,
    string Title,
    string Description,
    int YearOfRelease,
    string Director,
    int Duration,
    int AgeRestriction,
    List<Genre> Genres
) : IRequest<ErrorOr<MovieResponse>>;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, ErrorOr<MovieResponse>>
{
    private readonly IMovieRepository _movieRepository;

    public UpdateMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<ErrorOr<MovieResponse>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var exists = await _movieRepository.ExistsAsync(request.Id, cancellationToken);

        if (!exists) return Errors.Movies.InvalidId;

        var movie = request.MapToMovie();
        
        await _movieRepository.UpdateAsync(movie, cancellationToken);

        return movie.MapToMovieResponse();
    }
}

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(300);
        RuleFor(x => x.Director)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Duration)
            .GreaterThan(0);
        RuleFor(x => x.AgeRestriction)
            .GreaterThan(0)
            .LessThan(21);
    }
}