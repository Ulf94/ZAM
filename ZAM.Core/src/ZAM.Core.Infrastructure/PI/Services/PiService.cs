namespace ZAM.Core.Infrastructure.PI.Services;

using global::MassTransit;
using Microsoft.Extensions.Logging;
using Application;

public sealed class PiService : IPiService
{
    private readonly IPublishEndpoint bus;
    private readonly ILogger<PiService> logger;

    public PiService(IPublishEndpoint bus, ILogger<PiService> logger)
        => (this.bus, this.logger) = (bus, logger);

    public async Task GetRoomTemperature(int roomNumber)
    {
        logger.LogInformation("Publishing message to get room's {roomNumber} temperature", roomNumber);

        var message = new Shared.Interfaces.GetRoomTemperature
        {
            RoomNumber = roomNumber,
        };

        await bus.Publish(message, CancellationToken.None);
        logger.LogInformation("Message published");
    }
}
