namespace ZAM.Core.Infrastructure;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Tahoma;
using ZAM.Core.Application;
using ZAM.Core.Infrastructure.PI.Services;
using Hangfire;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddHangfire(configuration);

        //services.AddScoped<IPiService, PiService>();

        //services.AddMassTransit(configuration);
        services.AddTahoma(configuration);

        return services;
    }
}
