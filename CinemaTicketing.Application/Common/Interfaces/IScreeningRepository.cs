using CinemaTicketing.Domain.Screenings;

namespace CinemaTicketing.Application.Common.Interfaces;

public interface IScreeningRepository
{
    Task AddAsync(Screening screening, CancellationToken cancellationToken);
    Task<Screening?> GetByIdAsync(int screeningId, CancellationToken cancellationToken);
    Task<List<Screening>> ListByRoomAsync(int roomId, CancellationToken cancellationToken);
    Task<List<Screening>> ListByMovieAsync(int movieId, CancellationToken cancellationToken);
    Task RemoveAsync(Screening screening, CancellationToken cancellationToken);
    Task UpdateAsync(Screening screening, CancellationToken cancellationToken);
}