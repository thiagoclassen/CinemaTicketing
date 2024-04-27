using CinemaTicketing.Domain.Reservations;

namespace CinemaTicketing.Application.Common.Interfaces;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation, CancellationToken cancellationToken);
    Task<Reservation?> GetByIdAsync(int reservationId, CancellationToken cancellationToken);
    Task<List<Reservation?>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task DeleteAsync(Reservation reservation, CancellationToken cancellationToken);
    Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken);
}