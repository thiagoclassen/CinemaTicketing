using CinemaTicketing.Application.Interfaces;
using CinemaTicketing.Domain.Booking;
using CinemaTicketing.Infrastructure.Common;

namespace CinemaTicketing.Infrastructure.Bookings.Persistence;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Booking booking, CancellationToken cancellationToken)
    {
        await _context.Bookings.AddAsync(booking, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Booking?> GetByIdAsync(int bookingId, CancellationToken cancellationToken)
    {
        return await _context.Bookings.FindAsync(bookingId, cancellationToken);
    }
    
    public async Task RemoveAsync(Booking booking, CancellationToken cancellationToken)
    {
        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Booking booking, CancellationToken cancellationToken)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync(cancellationToken);
    }
}