using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Movies;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Movies.Commands;

public record CreateMovieCommand(
    string Title,
    string Description,
    int YearOfRelease,
    string Director,
    int Duration,
    int AgeRestriction,
    List<Genre> Genres
) : IRequest<ErrorOr<Movie>>;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, ErrorOr<Movie>>
{
    private readonly IMovieRepository _movieRepository;

    public CreateMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<ErrorOr<Movie>> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Title = command.Title,
            Description = command.Description,
            YearOfRelease = command.YearOfRelease,
            Director = command.Director,
            Duration = command.Duration,
            AgeRestriction = command.AgeRestriction,
            Genres = command.Genres
        };

        await _movieRepository.AddAsync(movie, cancellationToken);

        return movie;
    }
}

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
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