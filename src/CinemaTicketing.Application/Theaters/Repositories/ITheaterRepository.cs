using CinemaTicketing.Domain.Theaters;

namespace CinemaTicketing.Application.Theaters.Repositories;

public interface ITheaterRepository
{
    Task AddAsync(Theater theater, CancellationToken cancellationToken);
    Task<Theater?> GetByIdAsync(int theaterId, CancellationToken cancellationToken);
    Task<List<Theater>> ListAsync(CancellationToken cancellationToken);
    Task RemoveAsync(Theater theater, CancellationToken cancellationToken);
    Task UpdateAsync(Theater theater, CancellationToken cancellationToken);
}