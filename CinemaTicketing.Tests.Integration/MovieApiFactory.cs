using CinemaTicketing.API;
using CinemaTicketing.Infrastructure.Common;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;

namespace CinemaTicketing.Tests.Integration;

public class MovieApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private const int TestDbPort = 1433;

    private readonly MsSqlContainer _dbContainer =
        new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("yourStrong(!)Password")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithPortBinding(TestDbPort)
            .WithName("test-cinema-ticketing")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(TestDbPort))
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(AppDbContext));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_dbContainer.GetConnectionString());
            });
        });
        builder.UseEnvironment("Staging");
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}