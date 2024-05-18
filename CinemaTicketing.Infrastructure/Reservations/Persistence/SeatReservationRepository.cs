using CinemaTicketing.Application.Reservations.Repositories;
using CinemaTicketing.Domain.Reservations;
using CinemaTicketing.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.Infrastructure.Reservations.Persistence;

public class SeatReservationRepository : ISeatReservationRepository
{
    private readonly AppDbContext _context;

    public SeatReservationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SeatReservation seatReservation, CancellationToken cancellationToken)
    {
        await _context.SeatReservations.AddAsync(seatReservation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<SeatReservation?> GetByIdAsync(int seatReservationId, CancellationToken cancellationToken)
    {
        return await _context.SeatReservations.FindAsync([seatReservationId], cancellationToken);
    }

    public async Task<List<SeatReservation>> ListByReservationIdAsync(int reservationId,
        CancellationToken cancellationToken)
    {
        return await _context.SeatReservations
            .Where(sr => sr.ReservationId == reservationId)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(SeatReservation seatReservation, CancellationToken cancellationToken)
    {
        _context.SeatReservations.Update(seatReservation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(SeatReservation seatReservation, CancellationToken cancellationToken)
    {
        _context.SeatReservations.Remove(seatReservation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}