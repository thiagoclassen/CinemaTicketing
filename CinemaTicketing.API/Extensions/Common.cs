using CinemaTicketing.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketing.API.Extensions;

public static class Common
{
    public static void ApplyMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }

    public static void EnsureTestDbIsCreated(this IServiceProvider serviceProvider)
    {
        serviceProvider
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<AppDbContext>()?
            .Database
            .EnsureCreated();
    }
}