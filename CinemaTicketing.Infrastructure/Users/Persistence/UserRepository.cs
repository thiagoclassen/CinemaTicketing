using CinemaTicketing.Application.Interfaces;
using CinemaTicketing.Domain;
using CinemaTicketing.Infrastructure.Common;

namespace CinemaTicketing.Infrastructure.Users.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await _context.Users.FindAsync(userId , cancellationToken);
    }

    public async Task RemoveAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}