using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Theaters;
using CinemaTicketing.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Theaters.Persistence;

public class TheaterRepository : ITheaterRepository
{
    private readonly AppDbContext _context;

    public TheaterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Theater theater, CancellationToken cancellationToken)
    {
        await _context.Theaters.AddAsync(theater, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Theater?> GetByIdAsync(int theaterId, CancellationToken cancellationToken)
    {
        return await _context.Theaters.FindAsync([theaterId], cancellationToken);
    }

    public async Task<List<Theater>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context.Theaters.ToListAsync(cancellationToken);
    }

    public async Task RemoveAsync(Theater theater, CancellationToken cancellationToken)
    {
        _context.Remove(theater);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Theater theater, CancellationToken cancellationToken)
    {
        _context.Update(theater);
        await _context.SaveChangesAsync(cancellationToken);
    }
}