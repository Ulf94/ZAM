﻿namespace ZAM.Core.Infrastructure;

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Tahoma;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<TahomaCredentials>("TahomaCredentials");

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddMassTransit(configuration);
        services.AddTahoma(configuration);

        return services;
    }
}
