using CinemaTicketing.Application.Movies.Repositories;
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

    public async Task<Genre?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<List<Genre>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context.Genres.ToListAsync(cancellationToken);
    }
}