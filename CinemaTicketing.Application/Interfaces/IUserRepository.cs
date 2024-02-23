using CinemaTicketing.Domain;

namespace CinemaTicketing.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken);
    Task RemoveAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);   
}