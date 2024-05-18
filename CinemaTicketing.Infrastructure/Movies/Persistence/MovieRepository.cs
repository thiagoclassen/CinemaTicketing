using CinemaTicketing.Application.Movies.Repositories;
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
        // if (!movie.Genres.IsNullOrEmpty())
        //     movie.Genres.ForEach(genre => _context.Entry(genre).State = EntityState.Unchanged); // This is a hack to prevent EF from trying to insert a new genre

        List<Genre> trackedItems = [];
        foreach (var genre in movie.Genres)
        {
            var tracked = await _context.Genres.AsTracking().Where(g => g.Id == genre.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (tracked != null) trackedItems.Add(tracked);
        }

        movie.Genres.Clear();
        movie.Genres.AddRange(trackedItems);

        await _context.Movies.AddAsync(movie, cancellationToken);
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
        // if (!movie.Genres.IsNullOrEmpty())
        //     movie.Genres.ForEach(genre => _context.Entry(genre).State = EntityState.Unchanged); // This is a hack to prevent EF from trying to insert a new genre

        var trackedMovie = await _context.Movies.Include(m => m.Genres).AsTracking().Where(m => m.Id == movie.Id)
            .FirstOrDefaultAsync(cancellationToken);

        trackedMovie!.Title = movie.Title;
        trackedMovie.Description = movie.Description;
        trackedMovie.YearOfRelease = movie.YearOfRelease;
        trackedMovie.Director = movie.Director;
        trackedMovie.Duration = movie.Duration;
        trackedMovie.AgeRestriction = movie.AgeRestriction;
        trackedMovie.Genres = await _context.Genres.Where(g => movie.Genres.Select(x => x.Id).Contains(g.Id))
            .ToListAsync(cancellationToken);

        List<Genre> trackedItems = [];
        foreach (var genre in movie.Genres)
        {
            var tracked = await _context.Genres.AsTracking().Where(g => g.Id == genre.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (tracked != null) trackedItems.Add(tracked);
        }

        trackedMovie.Genres.Clear();
        trackedMovie.Genres.AddRange(trackedItems);


        // var trackedMovie = await _context.Movies.AsTracking().Where(m => m.Id == movie.Id).FirstOrDefaultAsync(cancellationToken);

        //_context.Update(movie);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int movieId, CancellationToken cancellationToken)
    {
        return await _context.Movies.AnyAsync(m => m.Id == movieId, cancellationToken);
    }
}