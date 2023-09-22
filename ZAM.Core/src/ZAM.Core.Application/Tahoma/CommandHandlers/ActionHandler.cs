namespace ZAM.Core.Application.Tahoma.CommandHandlers;

using Microsoft.Extensions.Logging;
using Services;

internal sealed class ActionHandler : IRequestHandler<Commands.Action>
{
    private readonly ILogger<ActionHandler> logger;
    private readonly ITahomaService tahomaService;

    public ActionHandler(ILogger<ActionHandler> logger, ITahomaService tahomaService)
        => (this.logger, this.tahomaService) = (logger, tahomaService);

    public Task Handle(Commands.Action request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("{HandlerName} started", nameof(ActionHandler));

        this.tahomaService.SendTahomaAction(request.Label, request.Command, request.DeviceUrl);

        return Task.CompletedTask;
    }
}
