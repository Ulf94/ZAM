namespace ZAM.Core.Application.Tahoma.QueryHandlers;

using Microsoft.Extensions.Logging;
using ZAM.Core.Application.Tahoma.Queries;
using ZAM.Core.Application.Tahoma.Services;

internal sealed class GetLoginCookieHandler : IRequestHandler<GetLoginCookie, string>
{
    private readonly ILogger<GetLoginCookieHandler> logger;
    private readonly ITahomaService tahomaService;

    public GetLoginCookieHandler(ILogger<GetLoginCookieHandler> logger, ITahomaService tahomaService)
        => (this.logger, this.tahomaService) = (logger, tahomaService);

    public async Task<string> Handle(GetLoginCookie request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("{HandlerName} started", nameof(GetLoginCookieHandler));

        var result = await this.tahomaService.GetLoginCookie();

        return result;
    }
}
