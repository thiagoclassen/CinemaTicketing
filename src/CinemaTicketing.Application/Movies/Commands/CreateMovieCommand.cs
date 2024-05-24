using CinemaTicketing.Application.Movies.Repositories;
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
    List<string> Genres
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
            Genres = command.Genres.Select(genre => new Genre(genre)).ToList()
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
        RuleFor(x => x.Genres)
            .Custom((genres, context) =>
            {
                foreach (var genre in genres.Where(genre => Genre.ListGenres().All(x => x.GenreName != genre)))
                    context.AddFailure("Genres", $"Genre {genre} is not valid. GET /api/genres for available genres.");
            });
    }
}