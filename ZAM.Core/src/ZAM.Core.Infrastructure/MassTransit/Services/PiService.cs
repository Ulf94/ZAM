namespace ZAM.Core.Infrastructure.MassTransit.Services;

using global::MassTransit;
using Microsoft.Extensions.Logging;
using ZAM.Core.Application;

public sealed class PiService : IPiService
{
    private const int DEFAULT_MESSAGE_EXPIRATION_IN_MINUTES = 30;
    private readonly IPublishEndpoint bus;
    private readonly ILogger<PiService> logger;

    public PiService(IPublishEndpoint bus, ILogger<PiService> logger)
        => (this.bus, this.logger) = (bus, logger);

    public async Task GetRoomTemperature(int roomNumber)
    {
        this.logger.LogInformation("Publishing message to get room's {roomNumber} temperature", roomNumber);

        var message = new ZAM.Shared.Interfaces.GetRoomTemperature
        {
            RoomNumber = roomNumber,
        };

        await this.bus.Publish<Shared.Interfaces.GetRoomTemperature>(message, CancellationToken.None);
        this.logger.LogInformation("Message published");
    }
}
