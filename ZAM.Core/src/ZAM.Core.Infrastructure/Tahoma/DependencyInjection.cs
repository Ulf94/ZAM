namespace ZAM.Core.Infrastructure.Tahoma;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZAM.Core.Application.Tahoma.Services;
using ZAM.Core.Infrastructure.Tahoma.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddTahoma(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(TahomaOptions.SectionName, config =>
        {
            config.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddMemoryCache();

        services.AddSingleton<ITahomaService, TahomaService>();

        var serviceProvider = services.BuildServiceProvider();
        var tahomaService = serviceProvider.GetRequiredService<ITahomaService>();

        return services;
    }
}
