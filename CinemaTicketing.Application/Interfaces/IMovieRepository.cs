using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Application.Interfaces;

public interface IMovieRepository
{
    Task AddAsync(Movie movie, CancellationToken cancellationToken);
    Task<Movie?> GetByIdAsync(int movieId, CancellationToken cancellationToken);
    Task<List<Movie>> ListAsync(CancellationToken cancellationToken);
    Task RemoveAsync(Movie movie, CancellationToken cancellationToken);
    Task UpdateAsync(Movie movie, CancellationToken cancellationToken);
}