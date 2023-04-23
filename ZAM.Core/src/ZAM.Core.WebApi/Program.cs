namespace ZAM.Core.WebApi;

using Serilog;
using ZAM.Core.Application;
using ZAM.Core.Infrastructure;

internal class Program
{
    public static int Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables(prefix: "CONFIG_")
            .Build();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var builder = WebApplication
            .CreateBuilder(args);

        builder.Host.UseSerilog(logger);

        builder.Logging.AddSerilog(logger);

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthorization();

        app.MapControllers();

        try
        {
            logger.Information("Application {ApplicationName} is starting", app.Environment.ApplicationName);

            app.Run();

            return 0;
        }
        catch (Exception exception)
        {
            logger.Error(exception, "{Message}", exception.Message);
            logger.Information("Application {ApplicationName} occurred an error and will be shut down", app.Environment.ApplicationName);

            return 1;
        }
        finally
        {
            logger.Information("Application {ApplicationName} is shutting down", app.Environment.ApplicationName);

            Log.CloseAndFlush();
        }
    }
}
