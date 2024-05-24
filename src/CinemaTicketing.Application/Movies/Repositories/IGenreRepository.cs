using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Application.Movies.Repositories;

public interface IGenreRepository
{
    Task<Genre?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Genre>> ListAsync(CancellationToken cancellationToken);
}