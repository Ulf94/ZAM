namespace ZAM.Core.Application.Tahoma.QueryHandlers;

using Microsoft.Extensions.Logging;
using Commands;
using Services;
using ViewModels;

internal class GetDevicesHandler : IRequestHandler<GetDevices, Dictionary<string, List<Device>>>
{
    private readonly ILogger<GetDevicesHandler> logger;
    private readonly ITahomaService tahomaService;

    public GetDevicesHandler(ILogger<GetDevicesHandler> logger, ITahomaService tahomaService)
        => (this.logger, this.tahomaService) = (logger, tahomaService);

    public async Task<Dictionary<string, List<Device>>> Handle(GetDevices request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("{HandlerName} started", nameof(GetDevicesHandler));

        var result = await this.tahomaService.GetDevices();

        return result;
    }
}
