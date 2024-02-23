using CinemaTicketing.Domain.Booking;

namespace CinemaTicketing.Application.Interfaces;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation, CancellationToken cancellationToken);
    Task<Reservation?> GetByIdAsync(int reservationId, CancellationToken cancellationToken);
    Task RemoveAsync(Reservation reservation, CancellationToken cancellationToken);
    Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken);
}