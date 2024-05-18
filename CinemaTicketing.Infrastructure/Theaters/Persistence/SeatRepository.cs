using CinemaTicketing.Application.Theaters.Repositories;
using CinemaTicketing.Domain.Theaters;
using CinemaTicketing.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Theaters.Persistence;

public class SeatRepository : ISeatRepository
{
    private readonly AppDbContext _context;

    public SeatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Seat seat, CancellationToken cancellationToken)
    {
        await _context.Seats.AddAsync(seat, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Seat?> GetByIdAsync(int seatId, CancellationToken cancellationToken)
    {
        return await _context.Seats.FindAsync([seatId], cancellationToken);
    }

    public async Task<List<Seat>> ListByRoomIdAsync(int roomId, CancellationToken cancellationToken)
    {
        return await _context.Seats
            .Where(s => s.RoomId == roomId)
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveAsync(Seat seat, CancellationToken cancellationToken)
    {
        _context.Seats.Remove(seat);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Seat seat, CancellationToken cancellationToken)
    {
        _context.Seats.Update(seat);
        await _context.SaveChangesAsync(cancellationToken);
    }
}