using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ZAM.Core.Infrastructure.Hangfire;

internal static class DependencyInjection
{
    public static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        var hangfireConnectionString = configuration.GetConnectionString("Hangfire");

        services.AddHangfire(config =>
        {
            config.UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangfireConnectionString);
        });

        services.AddHangfireServer();

        return services;
    }
}

