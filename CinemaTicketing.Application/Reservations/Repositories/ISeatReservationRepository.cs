using CinemaTicketing.Domain.Reservations;

namespace CinemaTicketing.Application.Reservations.Repositories;

public interface ISeatReservationRepository
{
    Task AddAsync(SeatReservation seatReservation, CancellationToken cancellationToken);
    Task<SeatReservation?> GetByIdAsync(int seatReservationId, CancellationToken cancellationToken);
    Task<List<SeatReservation>> ListByReservationIdAsync(int reservationId, CancellationToken cancellationToken);
    Task UpdateAsync(SeatReservation seatReservation, CancellationToken cancellationToken);
    Task RemoveAsync(SeatReservation seatReservation, CancellationToken cancellationToken);
}