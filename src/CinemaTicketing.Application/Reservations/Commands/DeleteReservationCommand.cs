using CinemaTicketing.Application.Reservations.Repositories;
using FluentValidation;
using MediatR;

namespace CinemaTicketing.Application.Reservations.Commands;

public record DeleteReservationCommand(int Id) : IRequest;

public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
{
    private readonly IReservationRepository _repository;

    public DeleteReservationCommandHandler(IReservationRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _repository.GetByIdAsync(request.Id, cancellationToken);
        // TODO - change to Result Pattern
        if (reservation is null)
            throw new Exception("Invalid movie Id.");

        await _repository.DeleteAsync(reservation, cancellationToken);
    }
}

public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
{
    public DeleteReservationCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }
}