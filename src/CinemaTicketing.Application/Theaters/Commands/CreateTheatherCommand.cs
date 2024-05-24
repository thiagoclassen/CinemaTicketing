using CinemaTicketing.Application.Theaters.Repositories;
using CinemaTicketing.Domain.Theaters;
using ErrorOr;
using MediatR;

namespace CinemaTicketing.Application.Theaters.Commands;

public record CreateTheatherCommand(
    string Name,
    string Address,
    List<Room> Rooms
) : IRequest<ErrorOr<Theater>>;

public class CreateTheatherCommandHandler : IRequestHandler<CreateTheatherCommand, ErrorOr<Theater>>
{
    private readonly ITheaterRepository _theaterRepository;

    public CreateTheatherCommandHandler(ITheaterRepository theaterRepository)
    {
        _theaterRepository = theaterRepository;
    }

    public async Task<ErrorOr<Theater>> Handle(CreateTheatherCommand command, CancellationToken cancellationToken)
    {
        var theater = new Theater
        {
            Name = command.Name,
            Address = command.Address,
            Rooms = command.Rooms
        };

        await _theaterRepository.AddAsync(theater, cancellationToken);

        return theater;
    }
}