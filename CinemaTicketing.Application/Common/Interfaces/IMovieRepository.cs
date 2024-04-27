using CinemaTicketing.Domain.Movies;

namespace CinemaTicketing.Application.Common.Interfaces;

public interface IMovieRepository
{
    Task AddAsync(Movie movie, CancellationToken cancellationToken);
    Task<Movie?> GetByIdAsync(int movieId, CancellationToken cancellationToken);
    Task<List<Movie>> ListAsync(CancellationToken cancellationToken);
    Task<int> DeleteAsync(Movie movie, CancellationToken cancellationToken);
    Task UpdateAsync(Movie movie, CancellationToken cancellationToken);
}