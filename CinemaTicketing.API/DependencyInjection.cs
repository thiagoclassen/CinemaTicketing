using Microsoft.AspNetCore.Http.Json;

namespace CinemaTicketing.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<JsonOptions>(options => { options.SerializerOptions.IncludeFields = true; });

        return services;
    }
}