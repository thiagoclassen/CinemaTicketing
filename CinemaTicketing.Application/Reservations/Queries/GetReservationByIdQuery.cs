using CinemaTicketing.Application.Reservations.Repositories;
using CinemaTicketing.Domain.Reservations;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Reservations.Queries;

public record GetReservationByIdQuery(int Id) : IRequest<Reservation?>;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, Reservation?>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByIdQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<Reservation?> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        return await _reservationRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}

public class GetReservationByIdQueryValidator : AbstractValidator<GetReservationByIdQuery>
{
    public GetReservationByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}