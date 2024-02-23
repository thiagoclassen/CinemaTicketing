using CinemaTicketing.Domain.Theaters;

namespace CinemaTicketing.Application.Interfaces;

public interface IRoomRepository
{
    Task AddAsync(Room room, CancellationToken cancellationToken);
    Task<Room?> GetByIdAsync(int roomId, CancellationToken cancellationToken);
    Task ListByTheaterIdAsync(int theaterId, CancellationToken cancellationToken);
    Task RemoveAsync(Room room, CancellationToken cancellationToken);
    Task UpdateAsync(Room room, CancellationToken cancellationToken);
}