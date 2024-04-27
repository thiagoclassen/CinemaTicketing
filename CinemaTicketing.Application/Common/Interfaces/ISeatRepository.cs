using CinemaTicketing.Domain.Theaters;

namespace CinemaTicketing.Application.Common.Interfaces;

public interface ISeatRepository
{
    Task AddAsync(Seat seat, CancellationToken cancellationToken);
    Task<Seat?> GetByIdAsync(int seatId, CancellationToken cancellationToken);
    Task<List<Seat>> ListByRoomIdAsync(int roomId, CancellationToken cancellationToken);
    Task RemoveAsync(Seat seat, CancellationToken cancellationToken);
    Task UpdateAsync(Seat seat, CancellationToken cancellationToken);
}