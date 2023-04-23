namespace ZAM.Core.Infrastructure.Tahoma;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZAM.Core.Application.Tahoma.Services;
using ZAM.Core.Infrastructure.Tahoma.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddTahoma(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("Tahoma").Get<TahomaOptions>()!;

        services.AddHttpClient(TahomaOptions.SectionName);

        services.AddMemoryCache();

        services.AddSingleton<ITahomaService, TahomaService>();

        var serviceProvider = services.BuildServiceProvider();
        var tahomaService = serviceProvider.GetRequiredService<ITahomaService>();

        //tahomaService.GetLoginCookie();
        //tahomaService.GetToken();
        //tahomaService.ActivateToken();
        //tahomaService.GetDevices();

        return services;
    }
}
