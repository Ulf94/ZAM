namespace ZAM.Core.WebApi;

using Serilog;
using Application;
using Infrastructure;

internal class Program
{
    public static int Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables("CONFIG_")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var builder = WebApplication
            .CreateBuilder(args);

        builder.Host.UseSerilog(Log.Logger);

        builder.Logging.AddSerilog(Log.Logger);

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
            Log.Logger.Information("Application {ApplicationName} is starting", app.Environment.ApplicationName);

            app.Run();

            return 0;
        }
        catch (Exception exception)
        {
            Log.Logger.Fatal(exception, "{Message}", exception.Message);
            Log.Logger.Information("Application {ApplicationName} occurred an error and will be shut down", app.Environment.ApplicationName);

            return 1;
        }
        finally
        {
            Log.Logger.Information("Application {ApplicationName} is shutting down", app.Environment.ApplicationName);

            Log.CloseAndFlush();
        }
    }
}
