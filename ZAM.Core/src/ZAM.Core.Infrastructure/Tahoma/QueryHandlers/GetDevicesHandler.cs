namespace ZAM.Core.Infrastructure.Tahoma.QueryHandlers;

using Microsoft.Extensions.Logging;
using ZAM.Core.Application.Tahoma.Commands;
using ZAM.Core.Application.Tahoma.Services;
using ZAM.Core.Application.Tahoma.ViewModels;

internal class GetDevicesHandler : IRequestHandler<GetDevices, List<Device>>
{
    private readonly ILogger<GetDevicesHandler> logger;
    private readonly ITahomaService tahomaService;

    public GetDevicesHandler(ILogger<GetDevicesHandler> logger, ITahomaService tahomaService)
        => (this.logger, this.tahomaService) = (logger, tahomaService);

    public async Task<List<Device>> Handle(GetDevices request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("{HandlerName} started", nameof(GetDevicesHandler));

        var result = await this.tahomaService.GetDevices();

        return result;
    }
}
