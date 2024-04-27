using CinemaTicketing.Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Movies.Commands;

public record DeleteMovieCommand(int Id) : IRequest;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IMovieRepository _movieRepository;

    public DeleteMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.Id, cancellationToken);

        // TODO - create custom exception
        if (movie is null)
            throw new Exception("Invalid movie Id.");

        await _movieRepository.DeleteAsync(movie, cancellationToken);
    }
}

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}