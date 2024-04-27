using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Screenings;
using CinemaTicketing.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Screenings.Persistence;

public class ScreeningRepository : IScreeningRepository
{
    private readonly AppDbContext _context;

    public ScreeningRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Screening screening, CancellationToken cancellationToken)
    {
        await _context.Screenings.AddAsync(screening, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Screening?> GetByIdAsync(int screeningId, CancellationToken cancellationToken)
    {
        return await _context.Screenings.FindAsync([screeningId], cancellationToken);
    }

    public async Task<List<Screening>> ListByRoomAsync(int roomId, CancellationToken cancellationToken)
    {
        return await _context.Screenings.Where(s => s.RoomId == roomId).ToListAsync(cancellationToken);
    }

    public async Task<List<Screening>> ListByMovieAsync(int movieId, CancellationToken cancellationToken)
    {
        return await _context.Screenings.Where(s => s.MovieId == movieId).ToListAsync(cancellationToken);
    }

    public async Task RemoveAsync(Screening screening, CancellationToken cancellationToken)
    {
        _context.Screenings.Remove(screening);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Screening screening, CancellationToken cancellationToken)
    {
        _context.Screenings.Update(screening);
        await _context.SaveChangesAsync(cancellationToken);
    }
}