using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Movies;
using CinemaTicketing.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Movies.Persistence;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;

    public GenreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Genre genre, CancellationToken cancellationToken)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Genre?> GetByIdAsync(int genreId, CancellationToken cancellationToken)
    {
        return await _context.Genres.FindAsync([genreId], cancellationToken);
    }

    public async Task<List<Genre>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context.Genres.ToListAsync(cancellationToken);
    }

    public async Task RemoveAsync(Genre genre, CancellationToken cancellationToken)
    {
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Genre genre, CancellationToken cancellationToken)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync(cancellationToken);
    }
}