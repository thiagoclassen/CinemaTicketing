using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Reservations;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Reservations.Queries;

public record GetReservationByUserIdQuery(Guid Id) : IRequest<List<Reservation?>>;

public class GetReservationByUserIdQueryHandler : IRequestHandler<GetReservationByUserIdQuery, List<Reservation?>>
{
    private readonly IReservationRepository _reservationRepository;

    public GetReservationByUserIdQueryHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<List<Reservation?>> Handle(GetReservationByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _reservationRepository.GetByUserIdAsync(request.Id, cancellationToken);
    }
}

public class GetReservationByUserIdQueryValidator : AbstractValidator<GetReservationByUserIdQuery>
{
    public GetReservationByUserIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
    }
}