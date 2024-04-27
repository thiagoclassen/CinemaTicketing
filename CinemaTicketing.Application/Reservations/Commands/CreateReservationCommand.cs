using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Reservations;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Reservations.Commands;

public record CreateReservationCommand(
    bool Confirmed,
    DateTime ReservedUntil,
    Guid UserId,
    int ScreeningId,
    List<SeatReservation> SeatReservations
) : IRequest;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;

    public CreateReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = new Reservation
        {
            Confirmed = request.Confirmed,
            ReservedUntil = request.ReservedUntil,
            UserId = request.UserId,
            ScreeningId = request.ScreeningId,
            SeatReservations = request.SeatReservations
        };

        await _reservationRepository.AddAsync(reservation, cancellationToken);
    }
}

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x => x.Confirmed)
            .NotNull();
        RuleFor(x => x.ReservedUntil)
            .NotNull();
        RuleFor(x => x.UserId)
            .NotNull();
        RuleFor(x => x.ScreeningId)
            .NotNull();
        RuleFor(x => x.SeatReservations)
            .NotNull();
    }
}