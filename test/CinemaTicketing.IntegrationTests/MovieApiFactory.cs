using CinemaTicketing.Infrastructure.Common;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace CinemaTicketing.Tests.Integration;

// ReSharper disable once ClassNeverInstantiated.Global
public class MovieApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const int ContainerDbPort = 1433;

    private readonly MsSqlContainer _dbContainer =
        new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("yourStrong(!)Password")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithPortBinding(ContainerDbPort, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(ContainerDbPort))
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //base.ConfigureWebHost(builder);
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.RemoveAll(typeof(AppDbContext));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_dbContainer.GetConnectionString());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        // using var scope = Services.CreateScope();
        // var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // ctx.Database.GetConnectionString();
        // await ctx.Database.EnsureCreatedAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}