using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Application.Interfaces;

public interface IGenreRepository
{
    Task AddAsync(Genre genre, CancellationToken cancellationToken);
    Task<Genre?> GetByIdAsync(int genreId, CancellationToken cancellationToken);
    Task<List<Genre>> ListAsync(CancellationToken cancellationToken);
    Task RemoveAsync(Genre genre, CancellationToken cancellationToken);
    Task UpdateAsync(Genre genre, CancellationToken cancellationToken);
}