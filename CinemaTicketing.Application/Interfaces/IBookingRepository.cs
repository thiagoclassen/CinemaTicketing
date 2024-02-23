using CinemaTicketing.Domain.Booking;

namespace CinemaTicketing.Application.Interfaces;

public interface IBookingRepository
{
    Task AddAsync(Booking booking, CancellationToken cancellationToken);
    Task<Booking?> GetByIdAsync(int bookingId, CancellationToken cancellationToken);
    Task RemoveAsync(Booking booking, CancellationToken cancellationToken);
    Task UpdateAsync(Booking booking, CancellationToken cancellationToken);
}