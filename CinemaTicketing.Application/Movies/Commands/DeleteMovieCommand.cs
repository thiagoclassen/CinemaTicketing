using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace CinemaTicketing.Application.Movies.Commands;

public record DeleteMovieCommand(int Id) : IRequest<ErrorOr<int>>;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, ErrorOr<int>>
{
    private readonly IMovieRepository _movieRepository;

    public DeleteMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<ErrorOr<int>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.Id, cancellationToken);

        if (movie is null)
            return Errors.Movies.InvalidId;

        return await _movieRepository.DeleteAsync(movie, cancellationToken);
    }
}