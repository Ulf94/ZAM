namespace ZAM.Core.Application.Tahoma.QueryHandlers;

using Microsoft.Extensions.Logging;
using ZAM.Core.Application.Tahoma.Models;
using ZAM.Core.Application.Tahoma.Queries;
using ZAM.Core.Application.Tahoma.Services;

internal class GetTokenHandler : IRequestHandler<GetToken, Token>
{
    private readonly ILogger<GetTokenHandler> logger;
    private readonly ITahomaService tahomaService;

    public GetTokenHandler(ILogger<GetTokenHandler> logger, ITahomaService tahomaService)
        => (this.logger, this.tahomaService) = (logger, tahomaService);

    public async Task<Token> Handle(GetToken request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("{HandlerName} started", nameof(GetTokenHandler));

        var result = await this.tahomaService.GetToken();

        return result;
    }
}
