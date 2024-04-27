using CinemaTicketing.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Movies.Commands;

public record UpdateMovieCommand(
    int Id,
    string Title,
    string Description,
    string Director,
    int Duration,
    int AgeRestriction
) : IRequest;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
{
    private readonly IMovieRepository _movieRepository;

    public UpdateMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.Id, cancellationToken);

        // TODO - create custom exception
        if (movie is null)
            throw new Exception("Movie not found.");

        // movie.Title = request.Title};
        // movie.Description = request.Description;
        // movie.Director = request.Director;
        // movie.Duration = request.Duration;
        // movie.AgeRestriction = request.AgeRestriction;

        await _movieRepository.UpdateAsync(movie, cancellationToken);
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