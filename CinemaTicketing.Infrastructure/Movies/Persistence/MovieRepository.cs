using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Movies;
using CinemaTicketing.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Movies.Persistence;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Movie movie, CancellationToken cancellationToken)
    {
        await _context.AddAsync(movie, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Movie?> GetByIdAsync(int movieId, CancellationToken cancellationToken)
    {
        var movie = await _context
            .Movies
            .FirstOrDefaultAsync(m => m.Id == movieId, cancellationToken);

        if (movie is not null)
            await _context.Entry(movie).Collection(m => m.Genres).LoadAsync(cancellationToken);

        return movie;
    }

    public async Task<List<Movie>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context
            .Movies
            .Include(m => m.Genres)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Movie movie, CancellationToken cancellationToken)
    {
        _context.Movies.Remove(movie);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Movie movie, CancellationToken cancellationToken)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int movieId, CancellationToken cancellationToken)
    {
        return await _context.Movies.AnyAsync(m => m.Id == movieId, cancellationToken);
    }
}