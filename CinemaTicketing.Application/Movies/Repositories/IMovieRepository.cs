using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Application.Movies.Repositories;

public interface IMovieRepository
{
    Task AddAsync(Movie movie, CancellationToken cancellationToken);
    Task<Movie?> GetByIdAsync(int movieId, CancellationToken cancellationToken);
    Task<List<Movie>> ListAsync(CancellationToken cancellationToken);
    Task<int> DeleteAsync(Movie movie, CancellationToken cancellationToken);
    Task UpdateAsync(Movie movie, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int movieId, CancellationToken cancellationToken);
}