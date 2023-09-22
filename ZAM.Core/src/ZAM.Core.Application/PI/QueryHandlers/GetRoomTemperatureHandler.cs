namespace ZAM.Core.Application.PI.QueryHandlers;

using MassTransit;
using Microsoft.Extensions.Logging;
using Queries;

internal sealed class GetLoginHandler : IRequestHandler<GetRoomTemperature>
{
    private readonly IPublishEndpoint bus;
    private readonly ILogger<GetLoginHandler> logger;
    private readonly IPiService piService;

    public GetLoginHandler(IPublishEndpoint bus, ILogger<GetLoginHandler> logger, IPiService piService)
        => (this.bus, this.logger, this.piService) = (bus, logger, piService);

    public async Task Handle(GetRoomTemperature request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Calling service to get room {roomNumber} temperature", request.RoomNumber);

        await this.piService.GetRoomTemperature(request.RoomNumber);
    }
}
