using CinemaTicketing.Application.Interfaces;
using CinemaTicketing.Domain.Reservations;
using CinemaTicketing.Infrastructure.Common;

namespace CinemaTicketing.Infrastructure.Reservations.Persistence;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        await _context.Reservations.AddAsync(reservation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Reservation?> GetByIdAsync(int reservationId, CancellationToken cancellationToken)
    {
        return await _context.Reservations.FindAsync(reservationId, cancellationToken);
    }

    public async Task RemoveAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        _context.Reservations.Update(reservation);
        await _context.SaveChangesAsync(cancellationToken);
    }
}