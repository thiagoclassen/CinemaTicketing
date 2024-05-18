using CinemaTicketing.Application.Reservations.Repositories;
using CinemaTicketing.Domain.Reservations;
using MediatR;

namespace CinemaTicketing.Application.Reservations.Commands;

public record UpdateReservationCommand(
    int Id,
    bool Confirmed,
    DateTime ReservedUntil,
    List<SeatReservation> SeatReservations) : IRequest;

public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;

    public UpdateReservationCommandHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.Id, cancellationToken);
        if (reservation is null)
            throw new Exception("Invalid reservation Id.");

        // reservation.Confirmed = request.Confirmed;
        // reservation.ReservedUntil = request.ReservedUntil;
        // reservation.SeatReservations = request.SeatReservations;

        await _reservationRepository.UpdateAsync(reservation, cancellationToken);
    }
}