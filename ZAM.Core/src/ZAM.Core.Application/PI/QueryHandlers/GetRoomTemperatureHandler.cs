namespace ZAM.Core.Application.PI.QueryHandlers;

using Microsoft.Extensions.Logging;
using Queries;

internal sealed class GetRoomTemperatureHandler : IRequestHandler<GetRoomTemperature>
{
    private readonly ILogger<GetRoomTemperatureHandler> logger;

    public GetRoomTemperatureHandler(ILogger<GetRoomTemperatureHandler> logger)
        => (this.logger) = (logger);

    public async Task Handle(GetRoomTemperature request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Calling service to get room {roomNumber} temperature", request.RoomNumber);

    }
}
