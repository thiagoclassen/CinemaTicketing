using CinemaTicketing.Application.Common.Interfaces;
using CinemaTicketing.Domain.Theaters;
using CinemaTicketing.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Theaters.Persistence;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Room room, CancellationToken cancellationToken)
    {
        await _context.Rooms.AddAsync(room, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Room?> GetByIdAsync(int roomId, CancellationToken cancellationToken)
    {
        return await _context.Rooms.FindAsync([roomId], cancellationToken);
    }

    public async Task ListByTheaterIdAsync(int theaterId, CancellationToken cancellationToken)
    {
        await _context.Rooms.Where(r => r.TheaterId == theaterId).ToListAsync(cancellationToken);
    }

    public async Task RemoveAsync(Room room, CancellationToken cancellationToken)
    {
        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Room room, CancellationToken cancellationToken)
    {
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync(cancellationToken);
    }
}